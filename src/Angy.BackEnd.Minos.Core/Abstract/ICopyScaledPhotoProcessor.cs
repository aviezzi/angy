using System.Threading.Tasks;
using Angy.BackEnd.Minos.Data.Model;

namespace Angy.BackEnd.Minos.Core.Abstract
{
    public interface ICopyScaledPhotoProcessor
    {
        Task<bool> ScaleAsync(Story story);
    }
}