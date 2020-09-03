using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Abstract;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Minos.Core.Clients
{
    public class KafkaClient : IKafkaClient
    {
        readonly string _topic;
        readonly ILogger<KafkaClient> _logger;

        readonly ProducerConfig _config;

        public KafkaClient(string bootServers, string topic, ILogger<KafkaClient> logger)
        {
            _config = new ProducerConfig { BootstrapServers = bootServers };
            _topic = topic;

            _logger = logger;
        }

        public async Task ProduceAsync<T>(T entity)
        {
            using var producer = new ProducerBuilder<Null, T>(_config).Build();
            await producer.ProduceAsync(_topic, new Message<Null, T> { Value = entity });
        }
    }
}