using System.Threading.Tasks;

namespace Angy.BackEnd.Minos.Core.Abstract
{
    public interface IKafkaClient
    {
        Task ProduceAsync<T>(T entity);
    }
}