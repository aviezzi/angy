using Angy.Model;
using Angy.ProductServer.Core;
using Angy.ProductServer.Core.Inputs;
using Angy.ProductServer.Core.RootTypes;
using Angy.ProductServer.Core.Types;
using Angy.Server.Data.Abstract;
using Angy.Server.Data.Repositories;
using Autofac;

namespace Angy.ProductServer.IoC
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
            builder.RegisterType<AttributeRepository>().As<IRepository<Attribute>>().InstancePerDependency();
            builder.RegisterType<MicroCategoryRepository>().As<IAttributeDescriptionRepository>().InstancePerDependency();
        }
    }
}