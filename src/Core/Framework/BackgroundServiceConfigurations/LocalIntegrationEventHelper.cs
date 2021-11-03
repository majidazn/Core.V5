using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.BackgroundServiceConfigurations
{
    public class LocalIntegrationEventHelper<T>
    {
        public long Id { get; set; }
        public Guid MessageId { get; set; }
        public T @Event { get; set; }
    }

    public class LocalIntegrationEventAndDomainModel<T, S> : LocalIntegrationEventHelper<T>
    {
        public S DomainModel { get; set; }
    }
}
