using Framework.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceBus.LocalIntegration.Repositories
{
    public interface IBackgroundTaskLocalIntegrationEventRepository : IRepository<LocalIntegrationEvent>
    {
    }
}
