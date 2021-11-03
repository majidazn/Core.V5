using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Enums
{
    public enum EnumLocalIntegrationEvent
    {
        //Pending = 0,
        //Ready = 1,
        //Done = 2

        Pending = 0,
        ReadyToPublish = 1,
        InQueueToProcess = 2,
        Published = 3,
        Error = 4
    }
}
