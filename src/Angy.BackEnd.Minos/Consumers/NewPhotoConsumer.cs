using System.Threading;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Requests;
using Angy.BackEnd.Minos.Data.Model;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Angy.BackEnd.Minos.Consumers
{
    public class NewPhotoConsumer : BackgroundService
    {
        readonly IMediator _mediator;

        public NewPhotoConsumer(IMediator mediator)
        {
            _mediator = mediator;
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

            consumer.Subscribe("topic");

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                var message = new NewPhotoRequest(result.Message.Value);
                await _mediator.Publish(message, stoppingToken);
            }

            consumer.Close();
        }
    }
}