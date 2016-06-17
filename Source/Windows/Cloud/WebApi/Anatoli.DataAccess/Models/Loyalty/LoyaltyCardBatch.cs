using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.Common.DataAccess.Models;

namespace Anatoli.DataAccess.Models
{
    public class LoyaltyCardBatch : BaseModel
    {
        public virtual ICollection<LoyaltyCard> LoyaltyCards { get; set; }
        public DateTime BatchGenerateDate { get; set; }
        public string BatchGeneratePDate { get; set; }
    }
}
