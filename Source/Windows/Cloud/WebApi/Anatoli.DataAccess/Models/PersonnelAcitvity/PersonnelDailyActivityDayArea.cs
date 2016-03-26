using Anatoli.DataAccess.Models.Route;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models.PersonnelAcitvity
{
    public class PersonnelDailyActivityDayArea : BaseModel
    {
        [ForeignKey("CompanyPersonnel")]
        public Guid CompanyPersonnelId { get; set; }
        public virtual CompanyPersonnel CompanyPersonnel { set; get; }
        [ForeignKey("RegionArea")]
        public Nullable<Guid> RegionAreaId { get; set; }
        public virtual RegionArea RegionArea { set; get; }
        public DateTime ActivityDate { get; set; }
        [StringLength(10)]
        public string ActivityPDate { get; set; }
    }
}
