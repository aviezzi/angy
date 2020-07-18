using System.Threading.Tasks;
using Angy.Client.Shared.Abstract;
using Angy.Client.Shared.Adapters;
using Angy.Client.Shared.Gateways;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Angy.Client.ProductDataManager
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton<IGraphQLClient>(sp => new GraphQLHttpClient("http://localhost:5000/graphql", new NewtonsoftJsonSerializer()));
            builder.Services.AddSingleton<IClientAdapter, ClientAdapter>();

            builder.Services.AddSingleton<ProductGateway>();
            builder.Services.AddSingleton<MicroCategoryGateway>();
            builder.Services.AddSingleton<AttributeGateway>();

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}