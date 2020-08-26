using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data.Model;
using NodaTime;

namespace Angy.BackEnd.Kharonte.Abstract
{
    public interface IKharonteWritingGateway
    {
        Task<IEnumerable<Photo>> SavePhotos(IEnumerable<Photo> pendingPhotos);

        Task DeleteOlderThan(Instant instant);
    }
}