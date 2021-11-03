using System.Collections.Generic;

namespace Common.ViewModels.KendoGrid
{
    public class GridState
    {
        public int skip { get; set; }
        public int take { get; set; }
        public HashSet<SortDescriptor> sort { get; set; }
        public Filter filter { get; set; }
    }
}
