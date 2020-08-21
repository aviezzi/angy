using System;
using System.Threading;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Kharonte
{
    public class Worker : BackgroundService
    {
        readonly KharonteContext _kharonte;
        readonly ILogger<Worker> _logger;

        public Worker(KharonteContext kharonte, ILogger<Worker> logger)
        {
            _kharonte = kharonte;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogError("Worker running at: {time}", DateTimeOffset.Now);
            }

            return Task.CompletedTask;
        }
    }
}