namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;    
    public class BasketNote : AnatoliBaseModel
    {
        //public int BasketNoteId { get; set; }
        //public int BasketId { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }
        [StringLength(500)]
        public string FullText { get; set; }
        public Nullable<DateTime> DueDate { get; set; }
        [StringLength(10)]
        public string DuePDate { get; set; }
        public Nullable<TimeSpan> DueTime { get; set; }
        public Nullable<byte> IsCompleted { get; set; }
    
        public virtual Basket Basket { get; set; }
    }
}
