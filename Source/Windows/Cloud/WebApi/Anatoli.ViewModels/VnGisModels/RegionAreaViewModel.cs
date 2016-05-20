using System;

namespace Anatoli.ViewModels.VnGisModels
{
    public class RegionAreaViewModel : BaseViewModel
    {
        public string AreaName { get; set; }
        public Guid RegionAreaLevelTypeId { get; set; }
        public bool IsLeaf { get; set; }
        public Guid? ParentId { get; set; }
        public int NLevel { get; set; }
        public int? Priority { get; set; }

    }
}
