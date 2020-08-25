using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Abstract
{
    public interface IKharonteReadingGateway
    {
        Task<Result<IEnumerable<Photo>, Error.Exceptional>> GetAccumulated();
    }
}