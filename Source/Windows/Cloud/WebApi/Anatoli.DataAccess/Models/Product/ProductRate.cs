namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class ProductRate : AnatoliBaseModel
    {
        public DateTime RateDate { get; set; }
        public TimeSpan RateTime { get; set; }
        public int Value { get; set; }
        public Nullable<Guid> RateBy { get; set; }
        public string RateByName { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public Guid ProductId { get; set; }
        [NotMapped]
        public double Avg { get; set; }

    }
}
