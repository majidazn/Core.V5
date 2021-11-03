using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class ReminderViewModel
    {
        
        public int ReminderId { get;  set; }
        public long? TicketId { get;  set; }
        public long? ReferenceId { get;  set; }
        public string Note { get;  set; }
        public int Type { get;  set; }
        public string TypeName { get; set; }
        public DateTime ReminderDate { get;  set; }
        public DateTime ReminderDT { get;  set; }
        public string ReminderTime  { get; set; }
    }
}
