using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class DistCompanyRegionLevelType : BaseModel
    {
        [StringLength(100)]
        public string DistCompanyRegionLevelTypeName { get; set; }
        public virtual ICollection<DistCompanyRegion> DistCompanyRegions { get; set; }
    }
}
