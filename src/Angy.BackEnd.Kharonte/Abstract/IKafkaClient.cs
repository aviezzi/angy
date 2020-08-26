using System.Threading.Tasks;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Abstract
{
    public interface IKafkaClient
    {
        Task<Result<Unit, Error.Exceptional>> ProduceAsync<T>(T entity) where T : EntityBase;
    }
}