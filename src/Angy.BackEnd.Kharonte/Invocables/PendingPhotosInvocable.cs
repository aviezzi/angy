using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Abstract;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.BackEnd.Kharonte.Options;
using Angy.Model;
using Coravel.Invocable;
using Microsoft.Extensions.Options;

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
            await DeleteOlderThanAsync();

            var accumulated = (await GetAccumulatedAsync()).Select(acc => acc.Path);

            var pending = RetrievePending(accumulated).ToImmutableArray();
            if (!pending.Any()) return;

            var validated = Validate(pending);

            var acquired = await AcquirePhotos(validated);

            var copied = (await CopyAsync(acquired)).ToArray();
            if (!copied.Any()) return;

            var sent = await SendAsync(copied);
        }

        async Task DeleteOlderThanAsync() => await _writingGateway.DeleteOlderThan(DateTimeOffset.Now.AddHours(_options.OlderThan));

        async Task<IEnumerable<Photo>> GetAccumulatedAsync()
        {
            var result = await _readingGateway.GetAccumulated();

            if (result.HasError()) ; // TODO: Handle

            return result.Success;
        }

        IEnumerable<Photo> RetrievePending(IEnumerable<string> accumulated)
        {
            var pendingPhotos = _ftpGateway.RetrievePendingPhotos(accumulated, _options.PhotoChunk);

            if (pendingPhotos.HasError()) ; // TODO: Handle, report to user.

            return pendingPhotos.Success;
        }

        IEnumerable<Photo> Validate(IEnumerable<Photo> photos)
        {
            var result = _validator.Validate(photos);

            if (result.HasError()) ; // TODO: Handle, report to user.

            return result.Success;
        }

        async Task<IEnumerable<Photo>> AcquirePhotos(IEnumerable<Photo> photos) => await _writingGateway.SavePendingPhotos(photos);

        async Task<IEnumerable<Photo>> CopyAsync(IEnumerable<Photo> persistedPhotos)
        {
            var result = await _ftpGateway.SavePhotos(persistedPhotos);

            if (result.HasError()) ; // TODO: Handle

            return result.Success;
        }

        async Task<IEnumerable<Photo>> SendAsync(IEnumerable<Photo> copied)
        {
            var result = await _acheronGateway.SendAsync(copied);

            if (result.HasError()) ; // TODO: Handle

            return result.Success;
        }
    }
}