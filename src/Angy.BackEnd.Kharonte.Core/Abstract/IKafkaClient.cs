using System.Threading.Tasks;
using Angy.Model;
using Angy.Model.Abstract;

namespace Angy.BackEnd.Kharonte.Core.Abstract
{
    public interface IKafkaClient
    {
        Task<IResult<Unit, Model.Error.Exceptional>> ProduceAsync<T>(T entity);
    }
}