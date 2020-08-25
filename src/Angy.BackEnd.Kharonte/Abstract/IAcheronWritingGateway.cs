using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.BackEnd.Kharonte.Model;

namespace Angy.BackEnd.Kharonte.Abstract
{
    public interface IAcheronWritingGateway
    {
        Task<Angy.Model.Result<IEnumerable<Photo>, IEnumerable<Error>>> SendAsync(IEnumerable<Photo> photos);
    }
}