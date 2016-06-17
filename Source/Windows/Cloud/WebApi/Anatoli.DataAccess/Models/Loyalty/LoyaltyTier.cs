using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models
{
    public class LoyaltyTier : BaseModel
    {
        [StringLength(100)]
        public string TierName { get; set; }
        public virtual ICollection<CustomerLoyaltyTierHistory> CustomerLoyaltyTierHistories { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
