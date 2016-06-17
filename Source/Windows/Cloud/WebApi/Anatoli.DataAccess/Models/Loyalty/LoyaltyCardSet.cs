using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.Common.DataAccess.Models;

namespace Anatoli.DataAccess.Models
{
    public class LoyaltyCardSet : BaseModel
    {
        [StringLength(100)]
        public string LoyaltyCardSetName { get; set; }
        public int Seed { get; set; }
        public long CurrentNo { get; set; }
        public string Initialtext { get; set; }
        public Guid GenerateNoType { get; set; }
        public virtual ICollection<LoyaltyCardBatch> LoyaltyCardBatchs { get; set; }
        public virtual ICollection<LoyaltyCard> LoyaltyCards { get; set; }
    }
}
