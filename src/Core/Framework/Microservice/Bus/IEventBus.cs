using Framework.Microservice.Commands;
using Framework.Microservice.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Microservice.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        Guid Publish<T>(T @event, Guid? messageId) where T : Event;

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }
}
