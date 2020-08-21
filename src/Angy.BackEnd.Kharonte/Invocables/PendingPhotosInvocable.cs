using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data;
using Angy.BackEnd.Kharonte.Data.Model;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Kharonte.Invocables
{
    public class PendingPhotosInvocable : IInvocable
    {
        readonly KharonteContext _kharonte;
        readonly ILogger<PendingPhotosInvocable> _logger;

        public PendingPhotosInvocable(KharonteContext kharonte, ILogger<PendingPhotosInvocable> logger)
        {
            _kharonte = kharonte;
            _logger = logger;
        }

        public async Task Invoke()
        {
            var pendingPhotos = Directory.GetFiles("C:\\PhotoManager\\source")
                .Select(file => new PendingPhoto
                {
                    Path = file,
                    Inserted = DateTimeOffset.Now
                })
                .Where(file => !_kharonte.PendingPhotos.Contains(file))
                .OrderBy(pending => pending.Inserted)
                .Take(count: 100);

            await _kharonte.AddRangeAsync(pendingPhotos);
            await _kharonte.SaveChangesAsync();
        }
    }
}