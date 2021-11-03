using Core.ServiceBus.Enums;
using Core.ServiceBus.LocalIntegration.Repositories;
using Core.ServiceBus.LocalIntegration.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceBus.LocalIntegration.Services
{
    public class BackgroundTaskLocalIntegrationEventService : IBackgroundTaskLocalIntegrationEventService
    {
        readonly IBackgroundTaskLocalIntegrationEventRepository _backgroundTaskLocalIntegrationEventRepository;
        public BackgroundTaskLocalIntegrationEventService(IBackgroundTaskLocalIntegrationEventRepository backgroundTaskLocalIntegrationEventRepository)
        {
            _backgroundTaskLocalIntegrationEventRepository = backgroundTaskLocalIntegrationEventRepository;
        }

        public async Task<List<LocalIntegrationEvent>> GetAllReadyToPulish()
        {
            var query = await _backgroundTaskLocalIntegrationEventRepository.FetchMulti(q => q.Status == (int)EnumLocalIntegrationEvent.ReadyToPublish)
                .OrderBy(q => q.Id)
                .Take(50)
                .ToListAsync();

            return query;
        }

        /// <summary>
        /// this method fires once when the BackgroundTask starts
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAllInQueueToProcessToReadyToPublish()
        {
            await _backgroundTaskLocalIntegrationEventRepository.BulkUpdateAsync(q => q.Status == (int)EnumLocalIntegrationEvent.InQueueToProcess,
                q => new LocalIntegrationEvent()
                {
                    Status = (int)EnumLocalIntegrationEvent.ReadyToPublish
                });
        }

        public async Task UpdateAsync(LocalIntegrationEvent model)
        {
            await _backgroundTaskLocalIntegrationEventRepository.UpdateAsync(model);
        }
    }
}
