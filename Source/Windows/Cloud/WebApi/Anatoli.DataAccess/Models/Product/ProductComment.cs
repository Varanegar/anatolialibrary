namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class ProductComment : BaseModel
    {
        public DateTime CommentDate { get; set; }
        public TimeSpan CommentTime { get; set; }
        public int Value { get; set; }
        public Nullable<Guid> CommentBy { get; set; }
        [StringLength(100)]
        public string CommentByName { get; set; }
        [StringLength(50)]
        public string CommentByEmailAddress { get; set; }
        public Nullable<byte> IsApproved { get; set; }
        //public Nullable<Guid> ProductId { get; set; }
        //public Guid ProductCommentId { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
