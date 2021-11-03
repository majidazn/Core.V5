using Common.ViewModels.KendoGrid;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Person
{
    public class GridPersonViewModel
    {
        public PersonViewModel filterModel { get; set; }
        public GridState gridState { get; set; }
    }
}
