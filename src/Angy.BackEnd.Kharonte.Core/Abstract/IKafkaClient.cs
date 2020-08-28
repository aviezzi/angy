using System.Threading.Tasks;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Core.Abstract
{
    public interface IKafkaClient
    {
        Task<Result<Unit, Model.Error.Exceptional>> ProduceAsync<T>(T entity);
    }
}