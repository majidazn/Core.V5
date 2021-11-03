using System.Collections.Generic;

namespace Common.ViewModels.KendoGrid
{
    public class Filter
    {
        public HashSet<FilterItems> filters { get; set; }
        public string logic { get; set; }
    }
}
