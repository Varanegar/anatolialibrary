namespace Anatoli.DataAccess.Models
{
	using System;
	using System.Collections.Generic;

    public class StoreDeliveryPerson : AnatoliBaseModel
    {
        public Nullable<byte> Status { get; set; }
    
        public virtual DeliveryPerson DeliveryPerson { get; set; }
        public virtual Store Store { get; set; }
    }
}
