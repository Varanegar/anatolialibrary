namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Clearance : BaseModel
    {
        //public int ClearanceId { get; set; }
        public Nullable<DateTime> ClearanceDate { get; set; }
        public string ClearancePDate { get; set; }
        public Nullable<TimeSpan> ClearanceTime { get; set; }
        public Nullable<int> CashierId { get; set; }
        public Nullable<int> CashSessionId { get; set; }
    }
}
