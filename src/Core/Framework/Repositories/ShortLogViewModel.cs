using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Repositories
{
    public class ShortLogViewModel
    {
        public DateTime ModifyDate { get; set; }
        public string OperatorName { get; set; }
        public string OperatorId { get; set; }
        public int Status { get; set; }
        public string IPAddress { get; set; }
    }
}
