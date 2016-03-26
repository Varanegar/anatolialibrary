namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class Clearance : BaseModel
    {
        public Nullable<DateTime> ClearanceDate { get; set; }
        [StringLength(10)]
        public string ClearancePDate { get; set; }
        public Nullable<TimeSpan> ClearanceTime { get; set; }
        public Nullable<int> CashierId { get; set; }
        public Nullable<int> CashSessionId { get; set; }
    }
}
