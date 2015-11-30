namespace Anatoli.DataAccess.Models
{
	using System;
	using System.Collections.Generic;

    public class StoreDeliveryPerson : BaseModel
    {
        public Nullable<byte> Status { get; set; }
        //public Nullable<Guid> StoreId { get; set; }
        //public Guid StoreDeliveryPersonId { get; set; }
        //public Nullable<Guid> DeliveryPersionId { get; set; }
    
        public virtual DeliveryPerson DeliveryPerson { get; set; }
        public virtual Store Store { get; set; }
    }
}