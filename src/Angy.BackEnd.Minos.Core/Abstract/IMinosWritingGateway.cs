using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Data.Model;

namespace Angy.BackEnd.Minos.Core.Abstract
{
    public interface IMinosWritingGateway
    {
        Task UpsertStoryAsync(Story story);

        Task UpsertStoriesAsync(IEnumerable<Story> story);
    }
}