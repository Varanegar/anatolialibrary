namespace Anatoli.DataAccess.Models
{
    using Anatoli.DataAccess.Models.PersonnelAcitvity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CompanyPersonnel : BaseModel
    {
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        public int Code { get; set; }
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("AnatoliAccount")]
        public Nullable<Guid> AnatoliAccountId { get; set; }
        public virtual AnatoliAccount AnatoliAccount { get; set; }
        public virtual ICollection<PersonnelDailyActivityDayArea> PersonnelDailyActivityDayAreas { get; set; }
        public virtual ICollection<PersonnelDailyActivityEvent> PersonnelDailyActivityEvents { get; set; }
        public virtual ICollection<PersonnelDailyActivityPoint> PersonnelDailyActivityPoints { get; set; }
    }
}
