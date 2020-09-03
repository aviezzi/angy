using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Data.Model;

namespace Angy.BackEnd.Minos.Core.Abstract
{
    public interface IMinosReadingGateway
    {
        Task<IEnumerable<Story>> GetStoriesForPhoto(Photo photo);

        Task<Story> GetStoryAsync(Guid id);
    }
}