namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class DeliveryPerson : AnatoliBaseModel
    {
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
    
        public virtual ICollection<StoreDeliveryPerson> StoreDeliveryPersons { get; set; }
    }
}
