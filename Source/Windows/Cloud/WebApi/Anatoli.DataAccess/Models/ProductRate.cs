namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class ProductRate : BaseModel
    {
        //public int ProductRateId { get; set; }
        //public Nullable<Guid> ProductId { get; set; }
        public DateTime RateDate { get; set; }
        public TimeSpan RateTime { get; set; }
        public int Value { get; set; }
        public Nullable<Guid> RateBy { get; set; }
        public string RateByName { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
