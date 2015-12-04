using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.DataAccess.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models
{
    public abstract class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public int Number_ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsRemoved { get; set; }

        public virtual Principal AddedBy { get; set; }
        public virtual Principal LastModifiedBy { get; set; }
        public virtual Principal PrivateLabelOwner { get; set; }
    }
}