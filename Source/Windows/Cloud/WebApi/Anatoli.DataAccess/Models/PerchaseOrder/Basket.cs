namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;  
      
    public class Basket : AnatoliBaseModel
    {       
        public Guid BasketTypeValueGuid { get; set; }
        [StringLength(200)]
        public string BasketName { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<BasketNote> BasketNotes { get; set; }
    }
}
