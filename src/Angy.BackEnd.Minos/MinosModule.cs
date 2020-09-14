using System.Reflection;
using Angy.BackEnd.Minos.Core.Abstract;
using Angy.BackEnd.Minos.Core.Gateways;
using Angy.BackEnd.Minos.Core.Handlers;
using Angy.BackEnd.Minos.Core.Processors;
using Autofac;
using Autofac.Features.Variance;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Angy.BackEnd.Minos
{
    public class MinosModule : Autofac.Module
    {
        readonly IConfiguration _configuration;

        public MinosModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MinosReadingGateway>().As<IMinosReadingGateway>().InstancePerLifetimeScope();
            builder.RegisterType<MinosWritingGateway>().As<IMinosWritingGateway>().InstancePerLifetimeScope();
            builder.RegisterType<AcheronGateway>().WithParameters(new[]
            {
                new NamedParameter("bootServers", _configuration.GetValue<string>("MinosOptions:KafkaOptions:BootServers")),
                new NamedParameter("topic", _configuration.GetValue<string>("MinosOptions:KafkaOptions:TopicReprocessing")),
                new NamedParameter("retryCount", _configuration.GetValue<string>("MinosOptions:KafkaOptions:MessageRetryCount")),
                new NamedParameter("retryAttempt", _configuration.GetValue<string>("MinosOptions:KafkaOptions:MessageRetryAttempt"))
            });

            builder.RegisterType<CopyScaledPhotoProcessor>().WithParameters(
                    new[]
                    {
                        new NamedParameter("source", _configuration.GetValue<string>("MinosOptions:SourceDirectory")),
                        new NamedParameter("destination", _configuration.GetValue<string>("MinosOptions:RetouchedDirectory")),
                        new NamedParameter("quality", _configuration.GetValue<string>("MinosOptions:Quality"))
                    })
                .SingleInstance();

            builder.RegisterType<NewPhotoHandler>();
            builder.RegisterType<ReprocessingPhotoHandler>();

            AddMediatR(builder);
        }

        static void AddMediatR(ContainerBuilder builder)
        {
            // Enable polymorphic dispatching of requests, but note that this will conflict with generic pipeline behaviors
            builder.RegisterSource(new ContravariantRegistrationSource());

            // Mediator itself
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(CopyScaledPhotoProcessor).GetTypeInfo().Assembly).AsImplementedInterfaces(); // via assembly scan
        }
    }
}