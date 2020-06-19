using Angy.Core;
using Angy.Core.RootTypes;
using Angy.Core.Types;
using Autofac;

namespace Angy.Server.IoC
{
    public class AngyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Schema>().InstancePerLifetimeScope();
            builder.RegisterType<Query>().InstancePerLifetimeScope();
            builder.RegisterType<ProductType>().InstancePerLifetimeScope();
            builder.RegisterType<MicroCategoryType>().InstancePerLifetimeScope();
        }
    }
}