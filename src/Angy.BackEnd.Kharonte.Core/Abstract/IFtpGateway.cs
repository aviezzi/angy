using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Core.Errors;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model.Abstract;

namespace Angy.BackEnd.Kharonte.Core.Abstract
{
    public interface IFtpGateway
    {
        IResult<IEnumerable<Photo>, IEnumerable<Error>> RetrievePendingPhotos(IEnumerable<string> accumulatedPaths, string source, int chunk);

        Task<IResult<IEnumerable<Photo>, IEnumerable<Error>>> CopyPhotosAsync(IEnumerable<Photo> source, string destination);

        IResult<IEnumerable<Photo>, IEnumerable<Error>> DeletePhotos(IEnumerable<Photo> photos);
    }
}