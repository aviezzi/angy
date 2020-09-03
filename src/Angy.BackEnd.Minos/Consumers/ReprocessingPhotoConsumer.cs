using System.Threading;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Requests;
using Angy.BackEnd.Minos.Data.Model;
using Angy.BackEnd.Minos.Options;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Angy.BackEnd.Minos.Consumers
{
    public class ReprocessingPhotoConsumer : BackgroundService
    {
        readonly IMediator _mediator;
        readonly MinosOptions _options;

        public ReprocessingPhotoConsumer(IMediator mediator, IOptions<MinosOptions> options)
        {
            _mediator = mediator;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "host1:9092,host2:9092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, Photo>(config).Build();

            consumer.Subscribe("reprocessing");

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                var message = new ReprocessingPhotoRequest(result.Message.Value, _options.Retrieves);
                await _mediator.Publish(message, stoppingToken);
            }

            consumer.Close();
        }
    }
}