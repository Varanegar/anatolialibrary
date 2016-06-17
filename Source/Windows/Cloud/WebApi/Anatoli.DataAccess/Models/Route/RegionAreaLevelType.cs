using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models.Route
{
    public class RegionAreaLevelType : AnatoliBaseModel
    {
        [StringLength(100)]
        public string RegionAreaLevelTypeName { get; set; }
        public virtual ICollection<RegionArea> RegionAreas { get; set; }
    }
}
