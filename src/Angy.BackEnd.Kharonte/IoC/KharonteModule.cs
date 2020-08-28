using System.Collections.Generic;
using Angy.BackEnd.Kharonte.Core.Abstract;
using Angy.BackEnd.Kharonte.Core.Clients;
using Angy.BackEnd.Kharonte.Core.Gateways;
using Angy.BackEnd.Kharonte.Core.Validators;
using Angy.BackEnd.Kharonte.Invocables;
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Configuration;

namespace Angy.BackEnd.Kharonte.IoC
{
    public class KharonteModule : Module
    {
        readonly IConfiguration _configuration;

        public KharonteModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PendingPhotosInvocable>();

            builder.RegisterType<AggregatePhotoValidator>().As<IAggregateValidator>();
            builder.RegisterType<PhotoValidator.Filename>().As<PhotoValidator>();
            builder.RegisterType<PhotoValidator.Extension>().As<PhotoValidator>()
                .WithParameter("extensions", _configuration.GetValue<string>("Validation:SupportedExtensions"));

            builder.RegisterType<KharonteReadingGateway>().As<IKharonteReadingGateway>();
            builder.RegisterType<KharonteWritingGateway>().As<IKharonteWritingGateway>();

            builder.RegisterType<AcheronWritingGateway>().As<IAcheronWritingGateway>();
            builder.RegisterType<KafkaClient>().As<IKafkaClient>()
                .WithParameters(new List<Parameter>
                {
                    new NamedParameter("bootServers", _configuration.GetValue<string>("Kafka:Host").Trim()),
                    new NamedParameter("topic", _configuration.GetValue<string>("Kafka:Topic").Trim()
                    )
                });

            builder.RegisterType<FtpGateway>().As<IFtpGateway>();

            builder.RegisterType<AggregatePhotoValidator>().As<IAggregateValidator>();
        }
    }
}