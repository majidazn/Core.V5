using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels.KendoGrid
{
    public class GridDataResult<T>
    {
        public List<T> data { get; set; }
        public decimal total { get; set; }
        public T FilterObject { get; set; }
        public HashSet<string> VisibleColumns { get; set; }
        public GridState GridState { get; set; }
    }
}
