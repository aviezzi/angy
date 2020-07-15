using System.Threading.Tasks;
using Angy.Shared.Abstract;
using Angy.Shared.Adapters;
using Angy.Shared.Gateways;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Angy.BackEndClient
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

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}