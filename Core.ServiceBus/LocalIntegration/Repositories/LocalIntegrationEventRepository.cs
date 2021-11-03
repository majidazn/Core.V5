using Core.ServiceBus.Enums;
using Core.ServiceBus.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceBus.LocalIntegration.Repositories
{
    public class LocalIntegrationEventRepository : ILocalIntegrationEventRepository
    {
        readonly BackgroundTaskDbContext _dbContext;
        public LocalIntegrationEventRepository( BackgroundTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LocalIntegrationEvent> SaveLocalIntegrationEvent<T>(Guid messageId, T ticketDeletedEvent)
        {
            var jsonSerialization = ticketDeletedEvent.SerializeJson();
            var binarySerialization = ticketDeletedEvent.SerializeBinary();

            var localIntegrationEvent = new LocalIntegrationEvent()
            {
                JsonBoby = jsonSerialization,
                UniqueId = messageId,
                CreatedAt = DateTime.Now,
                Status = (int)EnumLocalIntegrationEvent.ReadyToPublish,
                BinaryBody = binarySerialization,
                ModelName = ticketDeletedEvent.GetType().Name,
                ModelNamespace = ticketDeletedEvent.GetType().Namespace
            };

            await _dbContext.LocalIntegrationEvents.AddAsync(localIntegrationEvent).ConfigureAwait(false);
            //await CreateAsyncUoW(localIntegrationEvent).ConfigureAwait(false);
            return localIntegrationEvent;
        }


        public async Task UpdateLocalIntegrationEvent(long eventId)
        {
            var localIntegrationEvent = await _dbContext.LocalIntegrationEvents.FirstOrDefaultAsync(q => q.Id == eventId);

            if (localIntegrationEvent == null)
                return;

            localIntegrationEvent.Status = (int)EnumLocalIntegrationEvent.ReadyToPublish;
            _dbContext.LocalIntegrationEvents.Update(localIntegrationEvent);
        }

    }
}
