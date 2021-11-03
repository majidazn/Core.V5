using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class WorkDownViewModel
    {
        public long WorkDownId { get; set; }
        public long TicketId { get; set; }
        public long? ReferenceId { get; set; }
        public int Action { get; set; }
        public string Note { get; set; }
        public string Title { get; set; }
    }
}
