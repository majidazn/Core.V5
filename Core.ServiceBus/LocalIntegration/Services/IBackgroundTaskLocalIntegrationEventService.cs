using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceBus.LocalIntegration.Services
{
    public interface IBackgroundTaskLocalIntegrationEventService
    {
        Task<List<LocalIntegrationEvent>> GetAllReadyToPulish();

        Task UpdateAllInQueueToProcessToReadyToPublish();

        Task UpdateAsync(LocalIntegrationEvent model);
    }
}
