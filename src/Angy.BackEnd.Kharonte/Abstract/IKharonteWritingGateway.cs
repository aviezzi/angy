using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data.Model;

namespace Angy.BackEnd.Kharonte.Abstract
{
    public interface IKharonteWritingGateway
    {
        Task<IEnumerable<Photo>> SavePendingPhotos(IEnumerable<Photo> pendingPhotos);

        Task DeleteOlderThan(DateTimeOffset dateTime);
    }
}