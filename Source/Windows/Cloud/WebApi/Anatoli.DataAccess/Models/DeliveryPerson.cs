namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class DeliveryPerson : BaseModel
    {
        //public Guid DeliveryPersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    
        public virtual ICollection<StoreDeliveryPerson> StoreDeliveryPersons { get; set; }
    }
}
