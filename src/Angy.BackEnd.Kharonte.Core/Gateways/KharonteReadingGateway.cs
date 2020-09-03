using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Core.Abstract;
using Angy.BackEnd.Kharonte.Data;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;
using Angy.Model.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Kharonte.Core.Gateways
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

        public Task<IResult<IEnumerable<Photo>, Error.Exceptional>> GetAccumulated()
        {
            return Result.Try(
                async () => (await _kharonte.PendingPhotos.ToListAsync()).AsEnumerable(),
                ex => _logger.LogError(ex, "Cannot get pending photos from database."));
        }
    }
}