using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Abstract;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Gateways
{
    public class AcheronWritingGateway : IAcheronWritingGateway
    {
        readonly IKafkaClient _client;

        public AcheronWritingGateway(IKafkaClient client)
        {
            _client = client;
        }

        public async Task<Result<IEnumerable<Photo>, IEnumerable<Model.Error>>> SendAsync(IEnumerable<Photo> photos)
        {
            var success = new List<Photo>();
            var errors = new List<Model.Error>();

            foreach (var photo in photos)
            {
                var result = await _client.ProduceAsync(photo);

                if (result.HasError())
                    errors.Add(new Model.Error.SendFailed());
                else
                    success.Add(photo);
            }

            return new Result<IEnumerable<Photo>, IEnumerable<Model.Error>>(success, errors);
        }
    }
}