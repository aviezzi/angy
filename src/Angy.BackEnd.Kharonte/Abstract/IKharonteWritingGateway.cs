using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data.Model;
using NodaTime;

namespace Angy.BackEnd.Kharonte.Abstract
{
    public interface IKharonteWritingGateway
    {
        Task<IEnumerable<Photo>> SavePhotosAsync(IEnumerable<Photo> pendingPhotos);

        Task DeleteOlderThanAsync(Instant instant);

        Task<IEnumerable<Photo>> DeletePhotosAsync(IEnumerable<Photo> photos);

        Task LogErrorAsync(IEnumerable<PhotoError> photos);
    }
}