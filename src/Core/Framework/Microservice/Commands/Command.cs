

using Framework.Microservice.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Microservice.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
