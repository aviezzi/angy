using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Core.Abstract;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Core.Gateways
{
    public class AcheronWritingGateway : IAcheronWritingGateway
    {
        readonly IKafkaClient _client;

        public AcheronWritingGateway(IKafkaClient client)
        {
            _client = client;
        }

        public async Task<Result<IEnumerable<Photo>, IEnumerable<Error>>> SendAsync(IEnumerable<Photo> photos)
        {
            var success = new List<Photo>();
            var errors = new List<Error>();

            foreach (var photo in photos)
            {
                var result = await _client.ProduceAsync(photo);

                if (result.HasError())
                    errors.Add(new Error.SendFailed(photo));
                else
                    success.Add(photo);
            }

            return new Result<IEnumerable<Photo>, IEnumerable<Error>>(success, errors);
        }
    }
}