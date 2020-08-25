using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Abstract;
using Angy.BackEnd.Kharonte.Data;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Kharonte.Gateways
{
    public class KharonteReadingGateway : IKharonteReadingGateway
    {
        readonly KharonteContext _kharonte;
        readonly ILogger<KharonteReadingGateway> _logger;

        public KharonteReadingGateway(KharonteContext kharonte, ILogger<KharonteReadingGateway> logger)
        {
            _kharonte = kharonte;
            _logger = logger;
        }

        public Task<Result<IEnumerable<Photo>, Error.Exceptional>> GetAccumulated()
        {
            return Result.Try(
                async () => (await _kharonte.PendingPhotos.ToListAsync()).AsEnumerable(),
                ex => _logger.LogError(ex, "Cannot get pending photos from database."));
        }
    }
}