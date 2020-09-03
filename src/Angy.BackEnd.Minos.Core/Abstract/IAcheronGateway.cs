using System.Threading.Tasks;
using Angy.BackEnd.Minos.Data.Model;

namespace Angy.BackEnd.Minos.Core.Abstract
{
    public interface IAcheronGateway
    {
        Task SendAsync(Photo photos);
    }
}