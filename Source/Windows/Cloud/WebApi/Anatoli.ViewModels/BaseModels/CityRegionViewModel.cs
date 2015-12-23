using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.BaseModels
{
    public class CityRegionViewModel : BaseViewModel
    {
        public Guid ParentId { get; set; }
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public int? Priority { get; set; }
    }
}
