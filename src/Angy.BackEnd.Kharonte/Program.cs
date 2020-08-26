using Angy.BackEnd.Kharonte.Data;
using Angy.BackEnd.Kharonte.Invocables;
using Angy.BackEnd.Kharonte.IoC;
using Angy.BackEnd.Kharonte.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Coravel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Angy.BackEnd.Kharonte
{
    public class Program
    {
        static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Services.UseScheduler(scheduler =>
                scheduler
                    .Schedule<PendingPhotosInvocable>()
                    .EverySecond()
                    .PreventOverlapping("PendingPhotos"));

            host.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new KharonteModule());
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var env = hostContext.HostingEnvironment.EnvironmentName;

                    Configuration = new ConfigurationBuilder()
                        .SetBasePath(hostContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();

                    services.Configure<KharonteOptions>(Configuration.GetSection("KharonteOptions"));

                    services.AddScheduler();

                    services.AddDbContext<KharonteContext>(
                        options => options.UseNpgsql(Configuration.GetConnectionString("Kharonte"),
                            npgsqlOptions => npgsqlOptions.UseNodaTime()));
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration)
                        .CreateLogger();

                    loggingBuilder.AddSerilog(logger, dispose: true);
                });
    }
}