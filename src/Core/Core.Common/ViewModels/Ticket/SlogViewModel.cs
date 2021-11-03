using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Ticket
{
    public class SlogViewModel
    {
        public long OperatorId { get; set; }
        public DateTime Date { get; set; }
        public string OperatorName { get; set; }
        public int Status { get; set; }
    }
}
