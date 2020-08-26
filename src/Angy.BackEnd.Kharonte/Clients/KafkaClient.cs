using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Abstract;
using Angy.BackEnd.Kharonte.Options;
using Angy.Model;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Angy.BackEnd.Kharonte.Clients
{
    public class KafkaClient : IKafkaClient
    {
        readonly KafkaOptions _options;
        readonly ILogger<KafkaClient> _logger;

        readonly ProducerConfig _config;

        public KafkaClient(IOptions<KafkaOptions> options, ILogger<KafkaClient> logger)
        {
            _options = options.Value;
            _logger = logger;

            _config = new ProducerConfig { BootstrapServers = _options.BootServers };
        }

        public Task<Result<Unit, Angy.Model.Error.Exceptional>> ProduceAsync<T>(T entity)
        {
            return Result.Try(async () =>
                {
                    using var producer = new ProducerBuilder<Null, T>(_config).Build();
                    await producer.ProduceAsync(_options.Topic, new Message<Null, T> { Value = entity });
                },
                ex =>
                {
                    _logger.LogError(ex, "Sending failed!");
                });
        }
    }
}