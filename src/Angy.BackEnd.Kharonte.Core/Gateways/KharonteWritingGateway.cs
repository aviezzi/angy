using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Core.Abstract;
using Angy.BackEnd.Kharonte.Data;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;
using Microsoft.Extensions.Logging;
using NodaTime;

namespace Angy.BackEnd.Kharonte.Core.Gateways
{
    public class KharonteWritingGateway : IKharonteWritingGateway
    {
        readonly KharonteContext _kharonte;
        readonly ILogger<KharonteWritingGateway> _logger;

        public KharonteWritingGateway(KharonteContext kharonte, ILogger<KharonteWritingGateway> logger)
        {
            _kharonte = kharonte;
            _logger = logger;
        }

        public async Task<IEnumerable<Photo>> SavePhotosAsync(IEnumerable<Photo> pendingPhotos)
        {
            var photos = pendingPhotos.ToList();

            foreach (var photo in photos)
            {
                var inserted = await _kharonte.AddAsync(photo);

                photo.Id = inserted.Entity.Id;
            }

            var result = await Result.Try(
                async () => await _kharonte.SaveChangesAsync(),
                ex => _logger.LogError(ex, $"Cannot persist photos to database. ${string.Join(separator: ';', photos.Select(p => p.Path))}")
            );

            return result.HasError()
                ? new List<Photo>()
                : photos;
        }

        public async Task DeleteOlderThanAsync(Instant instant)
        {
            var toBeDelete = _kharonte.PendingPhotos.Where(photo => photo.Inserted < instant);
            _kharonte.PendingPhotos.RemoveRange(toBeDelete);

            await Result.Try(
                async () => await _kharonte.SaveChangesAsync(),
                ex => _logger.LogError(ex, $"Cannot delete older photos! {string.Join(separator: ';', toBeDelete.Select(p => p.Id))}"));
        }

        public async Task<IEnumerable<Photo>> DeletePhotosAsync(IEnumerable<Photo> photos)
        {
            var entities = photos.ToList();

            _kharonte.PendingPhotos.RemoveRange(entities);

            var result = await Result.Try(
                async () => await _kharonte.SaveChangesAsync(),
                ex => _logger.LogError(ex, $"Cannot delete older photos! {string.Join(separator: ';', entities.Select(p => p.Id))}"));

            return result.HasError()
                ? new List<Photo>()
                : entities;
        }

        public async Task LogErrorAsync(IEnumerable<PhotoError> photos)
        {
            var entities = photos.ToList();

            await _kharonte.PhotoErrors.AddRangeAsync(entities);

            await Result.Try(
                async () => await _kharonte.SaveChangesAsync(),
                ex => _logger.LogError(ex, $"Cannot delete older photos! {string.Join(separator: ';', entities.Select(p => p.Id))}"));
        }
    }
}