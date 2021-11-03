using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceBus.LocalIntegration.Repositories
{
    public interface ILocalIntegrationEventRepository
    {
        Task<LocalIntegrationEvent> SaveLocalIntegrationEvent<T>(Guid messageId, T ticketDeletedEvent);

        Task UpdateLocalIntegrationEvent(long eventId);
    }
}
