using System.Threading;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Requests;
using Angy.BackEnd.Minos.Data.Model;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Angy.BackEnd.Minos.Consumers
{
    public class NewPhotoConsumer : BackgroundService
    {
        readonly IMediator _mediator;
        readonly MinosOptions _options;

        public NewPhotoConsumer(IMediator mediator, IOptions<MinosOptions> options)
        {
            _mediator = mediator;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _options.KafkaOptions.BootServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe(_options.KafkaOptions.TopicNew);

            while (!stoppingToken.IsCancellationRequested)
            {
                // var result = consumer.Consume(stoppingToken);

                var message = new NewPhotoRequest(new Photo { Filename = "prova", Extension = ".jpg", Shot = ' ' });
                await _mediator.Publish(message, stoppingToken);
            }

            consumer.Close();
        }
    }
}