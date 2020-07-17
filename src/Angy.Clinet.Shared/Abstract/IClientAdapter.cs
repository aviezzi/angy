using System.Threading.Tasks;
using Angy.Model;
using Angy.Shared.Adapters;

namespace Angy.Shared.Abstract
{
    public interface IClientAdapter
    {
        Task<Result<TCast, Error.ExceptionalError>> SendQueryAsync<TResponse, TCast>(RequestAdapter<TResponse, TCast> adapter);
    }
}