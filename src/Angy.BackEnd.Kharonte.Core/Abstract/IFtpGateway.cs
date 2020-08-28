using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Core.Abstract
{
    public interface IFtpGateway
    {
        Result<IEnumerable<Photo>, IEnumerable<Error>> RetrievePendingPhotos(IEnumerable<string> accumulatedPaths, string source, int chunk);

        Task<Result<IEnumerable<Photo>, IEnumerable<Error>>> CopyPhotosAsync(IEnumerable<Photo> source, string destination);

        Result<IEnumerable<Photo>, IEnumerable<Error>> DeletePhotos(IEnumerable<Photo> photos);
    }
}