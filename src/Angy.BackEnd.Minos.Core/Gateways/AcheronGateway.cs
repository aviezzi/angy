using System;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Abstract;
using Angy.BackEnd.Minos.Data.Model;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Polly;

namespace Angy.BackEnd.Minos.Core.Gateways
{
    public class AcheronGateway : IAcheronGateway
    {
        readonly int _retryCount;
        readonly int _retryAttempt;
        readonly string _topic;
        readonly ILogger<AcheronGateway> _logger;

        readonly ProducerConfig _config;

        public AcheronGateway(string bootServers, string topic, int retryCount, int retryAttempt, ILogger<AcheronGateway> logger)
        {
            _config = new ProducerConfig { BootstrapServers = bootServers };
            _topic = topic;

            _logger = logger;
            _retryCount = retryCount;
            _retryAttempt = retryAttempt;
        }

        public async Task SendAsync(Photo photo) =>
            await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(_retryCount, retryAttempt => TimeSpan.FromSeconds(_retryAttempt))
                .ExecuteAsync(async () =>
                {
                    using var producer = new ProducerBuilder<Null, string>(_config).Build();
                    await producer.ProduceAsync(_topic, new Message<Null, string> { Value = $"Hi Alberto. It is {DateTime.Now.ToUniversalTime()}" });
                });
    }
}