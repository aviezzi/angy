using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Core.Abstract;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.BackEnd.Kharonte.Options;
using Angy.Model;
using Coravel.Invocable;
using Microsoft.Extensions.Options;
using NodaTime;

namespace Angy.BackEnd.Kharonte.Invocables
{
    public class PendingPhotosInvocable : IInvocable
    {
        readonly IKharonteReadingGateway _readingGateway;
        readonly IKharonteWritingGateway _writingGateway;
        readonly IAcheronWritingGateway _acheronGateway;
        readonly IFtpGateway _ftpGateway;

        readonly IAggregateValidator _validator;

        readonly KharonteOptions _options;

        public PendingPhotosInvocable(IKharonteReadingGateway readingGateway, IKharonteWritingGateway writingGateway, IAcheronWritingGateway acheronGateway, IFtpGateway ftpGateway, IAggregateValidator validator, IOptions<KharonteOptions> options)
        {
            _readingGateway = readingGateway;
            _writingGateway = writingGateway;
            _acheronGateway = acheronGateway;
            _ftpGateway = ftpGateway;

            _validator = validator;

            _options = options.Value;
        }

        public async Task Invoke()
        {
            await DeleteOlderPhotoAsync();

            var accumulated = (await GetAccumulatedPhotosAsync()).Select(acc => acc.Path);

            var pending = GetPendingPhotos(accumulated).ToImmutableArray();
            if (!pending.Any()) return;

            var validated = Validate(pending).ToImmutableArray();
            if (!validated.Any()) return;

            var acquired = (await SavePendingPhotosAsync(validated)).ToImmutableArray();
            if (!acquired.Any()) return;

            var copied = (await CopyAsync(acquired)).ToImmutableArray();
            if (!copied.Any()) return;

            var sent = (await SendAsync(copied)).ToImmutableArray();
            if (!sent.Any()) return;

            var deleted = await DeletePhotosAsync(sent);
            if (!deleted.Any()) return;

            Delete(sent);
        }

        async Task DeleteOlderPhotoAsync()
        {
            var utc = DateTime.Now.AddHours(-_options.OlderThan).ToUniversalTime();
            var interval = Instant.FromDateTimeUtc(utc);

            await _writingGateway.DeleteOlderThanAsync(interval);
        }

        async Task<IEnumerable<Photo>> GetAccumulatedPhotosAsync()
        {
            var result = await _readingGateway.GetAccumulated();

            if (result.HasError()) ; // Silent error, nothing to do caller return immediately.

            return result.Success;
        }

        IEnumerable<Photo> GetPendingPhotos(IEnumerable<string> accumulated)
        {
            var pendingPhotos = _ftpGateway.RetrievePendingPhotos(accumulated, _options.SourceDirectory, _options.PhotoChunk);

            if (pendingPhotos.HasError()) ; // Silent error, nothing to do! Photos will reprocess next schedule.

            return pendingPhotos.Success;
        }

        IEnumerable<Photo> Validate(IEnumerable<Photo> photos)
        {
            var result = _validator.Validate(photos);

            var errors = result.Error.ToImmutableArray();

            var invalidExtensions = errors
                .OfType<Core.Errors.Error.InvalidExtension>()
                .Select(error => new PhotoError { Extension = error.Photo.Extension, Filename = error.Photo.Filename, Message = "Invalid Extension!" })
                .ToList();

            var invalidFilenames = errors
                .OfType<Core.Errors.Error.InvalidFileName>()
                .Select(error => new PhotoError { Extension = error.Photo.Extension, Filename = error.Photo.Filename, Message = "Invalid Filename!" })
                .ToList();

            _writingGateway.LogErrorAsync(invalidExtensions.Concat(invalidFilenames));
            _ftpGateway.CopyPhotosAsync(errors.Select(error => error.Photo), _options.ErrorsDirectory);

            return result.Success;
        }

        Task<IEnumerable<Photo>> SavePendingPhotosAsync(IEnumerable<Photo> photos) => _writingGateway.SavePhotosAsync(photos);
        // Silent error, nothing to do! Photos will reprocess next schedule.

        async Task<IEnumerable<Photo>> CopyAsync(IEnumerable<Photo> photos)
        {
            var result = await _ftpGateway.CopyPhotosAsync(photos, _options.OriginalDirectory);

            if (result.HasError()) ; // Silent error, nothing to do! No copied photos will reprocess next schedule.

            return result.Success;
        }

        async Task<IEnumerable<Photo>> SendAsync(IEnumerable<Photo> photos)
        {
            var result = await _acheronGateway.SendAsync(photos);

            if (result.HasError()) ; // Silent error, nothing to do! No sent photos will reprocess next schedule.

            return result.Success;
        }

        Task<IEnumerable<Photo>> DeletePhotosAsync(ImmutableArray<Photo> sent) => _writingGateway.DeletePhotosAsync(sent);
        // Silent error, nothing to do! Photos locked for 24h.

        IEnumerable<Photo> Delete(IEnumerable<Photo> photos)
        {
            var result = _ftpGateway.DeletePhotos(photos);
            if (result.HasError()) ; // Silent error, nothing to do! No deleted photos will reprocess next schedule.
            return result.Success;
        }
    }
}