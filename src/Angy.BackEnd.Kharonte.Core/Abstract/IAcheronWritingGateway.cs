using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Core.Abstract
{
    public interface IAcheronWritingGateway
    {
        Task<Result<IEnumerable<Photo>, IEnumerable<Error>>> SendAsync(IEnumerable<Photo> photos);
    }
}