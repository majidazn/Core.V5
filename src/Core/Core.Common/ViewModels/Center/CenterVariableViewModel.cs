using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Center
{
    public class CenterVariableViewModel
    {
        public int CentervariableId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string EName { get; set; }
        public int Sort { get; set; }
        public int Type { get; set; }
        public int CenterId { get; set; }
        public int ParentId { get; set; }
        public int? CenterSubActivityId { get; set; }
    }
}
