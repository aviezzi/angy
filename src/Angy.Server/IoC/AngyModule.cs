using Angy.Core;
using Angy.Core.Abstract;
using Angy.Core.Inputs;
using Angy.Core.Repositories;
using Angy.Core.RootTypes;
using Angy.Core.Types;
using Angy.Shared.Model;
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
            builder.RegisterType<MicroCategoryInputType>().SingleInstance();

            builder.RegisterType<ProductRepository>().As<IRepository<Product>>().InstancePerDependency();
            builder.RegisterType<MicroCategoryRepository>().As<IRepository<MicroCategory>>().InstancePerDependency();
        }
    }
}