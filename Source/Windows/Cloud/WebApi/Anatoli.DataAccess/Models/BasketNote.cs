namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;    
    public class BasketNote : BaseModel
    {
        //public int BasketNoteId { get; set; }
        //public int BasketId { get; set; }
        public string Comment { get; set; }
        public string FullText { get; set; }
        public Nullable<DateTime> DueDate { get; set; }
        public string DuePDate { get; set; }
        public Nullable<TimeSpan> DueTime { get; set; }
        public Nullable<byte> IsCompleted { get; set; }
    
        public virtual Basket Basket { get; set; }
    }
}
