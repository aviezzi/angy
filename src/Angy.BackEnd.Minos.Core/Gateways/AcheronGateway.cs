using System;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Abstract;
using Angy.BackEnd.Minos.Data.Model;
using Polly;

namespace Angy.BackEnd.Minos.Core.Gateways
{
    public class AcheronGateway : IAcheronGateway
    {
        readonly IKafkaClient _client;
        readonly int _retryCount;
        readonly int _retryAttempt;

        public AcheronGateway(IKafkaClient client, int retryCount, int retryAttempt)
        {
            _client = client;
            _retryCount = retryCount;
            _retryAttempt = retryAttempt;
        }

        public async Task SendAsync(Photo photo) =>
            await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(_retryCount, retryAttempt => TimeSpan.FromSeconds(_retryAttempt))
                .ExecuteAsync(async () => await _client.ProduceAsync(photo));
    }
}