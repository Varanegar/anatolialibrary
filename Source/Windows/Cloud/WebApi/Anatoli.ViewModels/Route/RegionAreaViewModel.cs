using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.Route
{
    public class RegionAreaViewModel : BaseViewModel
    {
        public string AreaName { get; set; }
        public Guid RegionAreaLevelTypeId { get; set; }
        public bool IsLeaf { get; set; }
        public Guid? RegionAreaParentId { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }

    }
}
