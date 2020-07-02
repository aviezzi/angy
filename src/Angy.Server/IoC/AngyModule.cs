using Angy.Core;
using Angy.Core.Inputs;
using Angy.Core.RootTypes;
using Angy.Core.Types;
using Autofac;

namespace Angy.Server.IoC
{
    public class AngyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Schema>().SingleInstance();
            builder.RegisterType<Query>().SingleInstance();
            builder.RegisterType<Mutation>().SingleInstance();
            
            builder.RegisterType<ProductType>().SingleInstance();
            builder.RegisterType<ProductInputType>().SingleInstance();
            
            builder.RegisterType<MicroCategoryType>().SingleInstance();
        }
    }
}