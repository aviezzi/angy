using System;
using Angy.BackEnd.Kharonte.Data;
using Angy.BackEnd.Kharonte.Invocables;
using Coravel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Angy.BackEnd.Kharonte
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        
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
                .ConfigureServices((hostContext, services) =>
                {
                    var env = hostContext.HostingEnvironment.EnvironmentName;
                    
                    Configuration = new ConfigurationBuilder()
                        .SetBasePath(hostContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
                    
                    // services.AddHostedService<Worker>();
                    services.AddScheduler();
                    services.AddSingleton<PendingPhotosInvocable>();

                    services.AddDbContext<KharonteContext>(options =>
                        options.UseNpgsql(Configuration.GetConnectionString("Kharonte")), ServiceLifetime.Transient);
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    var level = Enum.TryParse("Error", out LogEventLevel parsed) ? parsed : LogEventLevel.Error;

                    var logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.Seq("http://localhost:5341", level)
                        .WriteTo.File("C:/PhotoManager/test.log", level)
                        .CreateLogger();

                    loggingBuilder.AddSerilog(logger, dispose: true);
                });
    }
}