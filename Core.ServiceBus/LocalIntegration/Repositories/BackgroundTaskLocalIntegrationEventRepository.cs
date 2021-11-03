using Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.LocalIntegration.Repositories
{
    public class BackgroundTaskLocalIntegrationEventRepository : Repository<LocalIntegrationEvent>, IBackgroundTaskLocalIntegrationEventRepository
    {
        public BackgroundTaskLocalIntegrationEventRepository(BackgroundTaskDbContext backgroundTaskDbContext) : base(backgroundTaskDbContext)
        {

        }
    }
}
