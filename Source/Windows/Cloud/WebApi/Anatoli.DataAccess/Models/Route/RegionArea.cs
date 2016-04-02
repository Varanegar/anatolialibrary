using Anatoli.DataAccess.Models.PersonnelAcitvity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models.Route
{
    public class RegionArea : BaseModel
    {
        [StringLength(200)]
        public string AreaName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }

        [ForeignKey("RegionAreaLevelTypeId")]
        public virtual RegionAreaLevelType RegionAreaLevelType { get; set; }

        public Guid RegionAreaLevelTypeId { get; set; }

        [ForeignKey("RegionAreaParentId")]
        public virtual RegionArea RegionAreaParent { get; set; }

        public Guid? RegionAreaParentId { get; set; }
        public virtual ICollection<RegionAreaPoint> RegionAreaPoints { get; set; }
        public virtual ICollection<CustomerArea> CustomerAreas { get; set; }
        public virtual ICollection<RegionArea> RegionAreaChilds { get; set; }
        public virtual ICollection<PersonnelDailyActivityDayArea> PersonnelDailyActivityDayAreas { get; set; }
        public bool IsLeaf { get; set; }
        public DbGeometry AreaLocation { get; set; }

    }
}
