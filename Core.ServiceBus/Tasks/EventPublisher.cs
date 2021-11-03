
using Castle.Core.Logging;
using Core.ServiceBus.Extensions;
using Core.ServiceBus.LocalIntegration;
using Core.ServiceBus.LocalIntegration.Services;
using Framework.BackgroundServiceConfigurations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.ServiceBus.Tasks
{
    public class EventPublisher : BackgroundService
    {


        readonly BackgroundServiceConfiguration _taskConfig;
        private readonly IBackgroundTaskLocalIntegrationEventService _backgroundTaskLocalIntegrationEventService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly BackgroundTaskDbContext _dbContext;
        private readonly ILogger<EventPublisher> _logger;

        public EventPublisher(
            IConfiguration configuration,
            IBackgroundTaskLocalIntegrationEventService backgroundTaskLocalIntegrationEventService,
            IPublishEndpoint publishEndpoint,
            BackgroundTaskDbContext backgroundTaskDbContext,
            ILogger<EventPublisher> logger)
        {
            _taskConfig = configuration.GetSection("Task_EventPublisher").Get<BackgroundServiceConfiguration>();
            this._backgroundTaskLocalIntegrationEventService = backgroundTaskLocalIntegrationEventService;
            _publishEndpoint = publishEndpoint;
            this._dbContext = backgroundTaskDbContext;
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(new TimeSpan(0, 0, _taskConfig.Interval), stoppingToken);

                await ProccessEvents();

                if (_taskConfig.IsFireAndForget == true)
                    break;
            }
            await Task.CompletedTask;
        }

        private async Task ProccessEvents()
        {
            List<LocalIntegrationEvent> localIntegrationEvents = await _backgroundTaskLocalIntegrationEventService.GetAllReadyToPulish();

            if (!localIntegrationEvents.Any()) return;

            try
            {
                foreach (var @event in localIntegrationEvents)
                {

                    object obj = @event.JsonBoby.DeserializeJson();
                    var messageId = @event.UniqueId;

                    var interfaces = obj.GetType().GetInterfaces();

                    await _publishEndpoint.Publish(obj, interfaces[0]);

                    @event.UniqueId = messageId;
                    @event.Status = (int)Enums.EnumLocalIntegrationEvent.Published;
                    await _backgroundTaskLocalIntegrationEventService.UpdateAsync(@event);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Publish LocalIntegrationEvent failed.", ex);
            }

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveChanges LocalIntegrationEvent failed.", ex);
            }

        }
    }
}
