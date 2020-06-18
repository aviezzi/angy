using Angy.Core;
using Angy.Core.RootTypes;
using Angy.Core.Types;
using GraphQL.Server;
using GraphQL.Server.Ui.Altair;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Angy.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Schema>();
            services.AddSingleton<Query>();
            services.AddSingleton<ProductType>();

            services.AddGraphQL(options =>
                {
                    options.EnableMetrics = Environment.IsDevelopment() || Environment.IsStaging();
                    options.ExposeExceptions = Environment.IsDevelopment() || Environment.IsStaging();
                })
                .AddSystemTextJson()
                .AddWebSockets()
                .AddDataLoader();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseWebSockets();

            // Enable endpoint for websockets (subscriptions)
            // Enable endpoint for querying
            app.UseGraphQLWebSockets<Schema>();
            app.UseGraphQL<Schema>();

            if (Environment.IsDevelopment() || Environment.IsStaging())
                // use altair middleware at default url /ui/altair
                app.UseGraphQLAltair(new GraphQLAltairOptions());
        }
    }
}