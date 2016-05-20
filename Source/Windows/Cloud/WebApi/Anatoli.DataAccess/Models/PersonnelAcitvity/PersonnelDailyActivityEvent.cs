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
    public class PersonnelDailyActivityEvent : BaseModel
    {
        [ForeignKey("CompanyPersonnel")]
        public Guid CompanyPersonnelId { get; set; }
        public virtual CompanyPersonnel CompanyPersonnel { set; get; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey("PersonnelDailyActivityVisitType")]
        public Guid PersonnelDailyActivityVisitTypeId { get; set; }
        public virtual PersonnelDailyActivityVisitType PersonnelDailyActivityVisitType { get; set; }
        [ForeignKey("PersonnelDailyActivityEventType")]
        public Guid PersonnelDailyActivityEventTypeId { get; set; }
        public virtual PersonnelDailyActivityEventType PersonnelDailyActivityEventType { get; set; }
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        [StringLength(1000)]
        public string ShortDescription { get; set; }
        public DateTime ActivityDate { get; set; }
        [StringLength(10)]
        public string ActivityPDate { get; set; }

        public string JData { get; set; }
    }
}
