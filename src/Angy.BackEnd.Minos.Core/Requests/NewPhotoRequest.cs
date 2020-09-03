using Angy.BackEnd.Minos.Data.Model;
using MediatR;

namespace Angy.BackEnd.Minos.Core.Requests
{
    public class NewPhotoRequest : INotification
    {
        public NewPhotoRequest(Photo photo)
        {
            Photo = photo;
        }

        public Photo Photo { get; }
    }
}