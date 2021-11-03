using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class ReceiverViewModel
    {
        public int? RecieverId { get;  set; }
        public long? TicketId { get;  set; }
        public long? ReferenceId { get;  set; }
        public int ResponsibleId { get;  set; }
        public int ResponsibleFullName  { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public int? Category { get;  set; }
        public string Note { get;  set; }
        public int RoleType { get; set; }
        public string RoleTypeName { get; set; }
        public int Position { get; set; }
        public string PositionName { get; set; }
        public bool IsRead { get; set; }
        public string ReadDateShamsi { get; set; }
        public string PastDate { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
