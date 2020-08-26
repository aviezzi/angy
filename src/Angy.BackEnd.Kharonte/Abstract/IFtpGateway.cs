using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.BackEnd.Kharonte.Model;

namespace Angy.BackEnd.Kharonte.Abstract
{
    public interface IFtpGateway
    {
        Angy.Model.Result<IEnumerable<Photo>, IEnumerable<Error>> RetrievePendingPhotos(IEnumerable<string> accumulatedPaths, int chunk);

        Task<Angy.Model.Result<IEnumerable<Photo>, IEnumerable<Error>>> CopyPhotosAsync(IEnumerable<Photo> photos);

        Angy.Model.Result<IEnumerable<Photo>, IEnumerable<Error>> DeletePhotos(IEnumerable<Photo> photos);
    }
}