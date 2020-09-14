using Angy.BackEnd.Minos.Consumers;
using Angy.BackEnd.Minos.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Angy.BackEnd.Minos
{
    public class Program
    {
        static IConfigurationRoot Configuration { get; set; } = null!;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new MinosModule(Configuration));
                })
                .ConfigureServices((hostContext, services) =>
                {
                    Configure(hostContext);

                    services.Configure<MinosOptions>(Configuration.GetSection("MinosOptions"));

                    services.AddDbContext<MinosContext>(
                        options => options.UseNpgsql(Configuration.GetConnectionString("Minos"),
                            npgsqlOptions => npgsqlOptions.UseNodaTime()));

                    services.AddHostedService<NewPhotoConsumer>();
                    services.AddHostedService<ReprocessingPhotoConsumer>();
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration)
                        .CreateLogger();

                    loggingBuilder.AddSerilog(logger, dispose: true);
                });

        static void Configure(HostBuilderContext hostContext)
        {
            var env = hostContext.HostingEnvironment.EnvironmentName;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(hostContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}