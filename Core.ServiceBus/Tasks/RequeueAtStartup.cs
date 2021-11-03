using Core.ServiceBus.LocalIntegration.Repositories;
using Core.ServiceBus.LocalIntegration.Services;
using Framework.BackgroundServiceConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.ServiceBus.Tasks
{
    public class RequeueAtStartup : BackgroundService
    {

        readonly BackgroundServiceConfiguration _taskConfig;
        readonly IBackgroundTaskLocalIntegrationEventService _backgroundTaskLocalIntegrationEventService;
        private readonly IBackgroundTaskLocalIntegrationEventRepository _backgroundTaskLocalIntegrationEventRepository;

        public RequeueAtStartup(IConfiguration configuration
            , IBackgroundTaskLocalIntegrationEventService backgroundTaskLocalIntegrationEventService,
            IBackgroundTaskLocalIntegrationEventRepository backgroundTaskLocalIntegrationEventRepository

            )
        {
            _taskConfig = configuration.GetSection("Task_RequeueAtStartup").Get<BackgroundServiceConfiguration>();
            _backgroundTaskLocalIntegrationEventService = backgroundTaskLocalIntegrationEventService;
            _backgroundTaskLocalIntegrationEventRepository = backgroundTaskLocalIntegrationEventRepository;
        }


        /// <summary>
        /// If TicketMicroservice is stopped by any exceptions or incidents, some IntegrationEvents that are in InQueue status, won't be published on next run of API
        /// this BackgroundService starts once, every time that BackgroundTasks Api starts, and it updates all InQueue statuses to ReadyToPublish 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(new TimeSpan(0, 0, _taskConfig.Interval), stoppingToken);


                await _backgroundTaskLocalIntegrationEventService.UpdateAllInQueueToProcessToReadyToPublish();
                await _backgroundTaskLocalIntegrationEventRepository.SaveChangesAsync();

                if (_taskConfig.IsFireAndForget == true)
                    break;
            }
            await Task.CompletedTask;
        }
    }
}
