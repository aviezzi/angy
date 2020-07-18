using Angy.Model;
using Angy.Server.Data.Abstract;
using Angy.Server.Data.Repositories;
using Angy.Server.Product.GraphQL;
using Angy.Server.Product.GraphQL.Inputs;
using Angy.Server.Product.GraphQL.RootTypes;
using Angy.Server.Product.GraphQL.Types;
using Autofac;

namespace Angy.Server.Product.IoC
{
    public class ProductModule : Module
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
            
            builder.RegisterType<AttributeType>().SingleInstance();
            builder.RegisterType<AttributeDescriptionType>().SingleInstance();

            builder.RegisterType<ProductRepository>().As<IRepository<Model.Product>>().InstancePerDependency();
            builder.RegisterType<MicroCategoryRepository>().As<IRepository<MicroCategory>>().InstancePerDependency();
            builder.RegisterType<AttributeRepository>().As<IRepository<Attribute>>().InstancePerDependency();
            builder.RegisterType<AttributeDescriptionRepository>().As<IAttributeDescriptionRepository>().InstancePerDependency();
        }
    }
}