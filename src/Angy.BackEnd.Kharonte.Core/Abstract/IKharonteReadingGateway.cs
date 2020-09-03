using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;
using Angy.Model.Abstract;

namespace Angy.BackEnd.Kharonte.Core.Abstract
{
    public interface IKharonteReadingGateway
    {
        Task<IResult<IEnumerable<Photo>, Error.Exceptional>> GetAccumulated();
    }
}