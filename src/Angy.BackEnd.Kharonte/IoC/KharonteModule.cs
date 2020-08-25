using Angy.BackEnd.Kharonte.Abstract;
using Angy.BackEnd.Kharonte.Clients;
using Angy.BackEnd.Kharonte.Gateways;
using Angy.BackEnd.Kharonte.Invocables;
using Angy.BackEnd.Kharonte.Validators;
using Autofac;

namespace Angy.BackEnd.Kharonte.IoC
{
    public class KharonteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PendingPhotosInvocable>();

            builder.RegisterType<PhotoValidator.Extension>().As<PhotoValidator>();
            builder.RegisterType<PhotoValidator.Filename>().As<PhotoValidator>();
            builder.RegisterType<AggregatePhotoValidator>().As<IAggregateValidator>();

            builder.RegisterType<KharonteReadingGateway>().As<IKharonteReadingGateway>();
            builder.RegisterType<KharonteWritingGateway>().As<IKharonteWritingGateway>();

            builder.RegisterType<AcheronWritingGateway>().As<IAcheronWritingGateway>();
            builder.RegisterType<KafkaClient>().As<IKafkaClient>();

            builder.RegisterType<FtpGateway>().As<IFtpGateway>();

            builder.RegisterType<AggregatePhotoValidator>().As<IAggregateValidator>();
        }
    }
}