using System.Threading.Tasks;
using Angy.Shared.Gateways;
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

            builder.Services.AddScoped(sp => new GraphQLHttpClient("http://localhost:5000/graphql", new NewtonsoftJsonSerializer()));

            builder.Services.AddScoped<ProductGateway>();
            builder.Services.AddScoped<MicroCategoryGateway>();

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}