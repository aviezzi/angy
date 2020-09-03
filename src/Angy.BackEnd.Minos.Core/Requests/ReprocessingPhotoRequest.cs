using Angy.BackEnd.Minos.Data.Model;
using MediatR;

namespace Angy.BackEnd.Minos.Core.Requests
{
    public class ReprocessingPhotoRequest : INotification
    {
        public ReprocessingPhotoRequest(Photo photo, int retrieves)
        {
            Photo = photo;
            Retrieves = retrieves;
        }

        public Photo Photo { get; }
        public int Retrieves { get; }
    }
}