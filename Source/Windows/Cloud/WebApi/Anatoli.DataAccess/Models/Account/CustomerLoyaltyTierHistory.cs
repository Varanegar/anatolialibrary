using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models
{
    public class CustomerLoyaltyTierHistory : BaseModel
    {
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey("LoyaltyTier")]
        public Guid LoyaltyTierId { get; set; }
        public virtual LoyaltyTier LoyaltyTier { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }
}
