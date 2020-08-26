using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Abstract;
using Angy.BackEnd.Kharonte.Data;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;
using Microsoft.Extensions.Logging;
using NodaTime;

namespace Angy.BackEnd.Kharonte.Gateways
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

        public async Task<IEnumerable<Photo>> SavePhotos(IEnumerable<Photo> pendingPhotos)
        {
            var photos = pendingPhotos as Photo[] ?? pendingPhotos.ToArray();

            foreach (var photo in photos)
            {
                var inserted = await _kharonte.AddAsync(photo);

                photo.Id = inserted.Entity.Id;
            }

            var result = await Result.Try(
                async () => await _kharonte.SaveChangesAsync(),
                ex => _logger.LogError(ex, $"Cannot persist photos to database. ${string.Join(separator: ';', photos.Select(p => p.Path))}")
            );

            if (result.HasError()) return new List<Photo>();

            return photos;
        }

        public async Task DeleteOlderThan(Instant instant)
        {
            var toBeDelete = _kharonte.PendingPhotos.Where(photo => photo.Inserted < instant);
            _kharonte.PendingPhotos.RemoveRange(toBeDelete);

            await Result.Try(
                async () => await _kharonte.SaveChangesAsync(),
                ex => _logger.LogError(ex, $"Cannot delete older photos! {string.Join(separator: ';', toBeDelete.Select(p => p.Id))}"));
        }
    }
}