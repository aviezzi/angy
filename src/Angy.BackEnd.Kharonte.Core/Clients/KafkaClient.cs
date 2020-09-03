using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Core.Abstract;
using Angy.Model;
using Angy.Model.Abstract;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Kharonte.Core.Clients
{
    public class KafkaClient : IKafkaClient
    {
        readonly string _topic;
        readonly ILogger<KafkaClient> _logger;

        readonly ProducerConfig _config;

        public KafkaClient(string bootServers, string topic, ILogger<KafkaClient> logger)
        {
            _topic = topic;
            _logger = logger;

            _config = new ProducerConfig { BootstrapServers = bootServers };
        }

        public Task<IResult<Unit, Model.Error.Exceptional>> ProduceAsync<T>(T entity)
        {
            return Result.Try(async () =>
                {
                    using var producer = new ProducerBuilder<Null, T>(_config).Build();
                    await producer.ProduceAsync(_topic, new Message<Null, T> { Value = entity });
                },
                ex =>
                {
                    _logger.LogError(ex, "Sending failed!");
                });
        }
    }
}