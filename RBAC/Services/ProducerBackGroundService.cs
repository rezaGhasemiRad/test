using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mail.Services
{
    public class ProducerBackGroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ProducerBackGroundService> _logger;

        public ProducerBackGroundService(IServiceScopeFactory serviceScopeFactory, ILogger<ProducerBackGroundService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DoWork();
                
                await Task.Delay(1000);
            };
        }

        private void DoWork()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
            publishEndpoint.Publish(new global::Models.Mail
            {
                Title = "Test",
                Message = "Time : " + DateTime.Now
            });
        }
    }
}