using System;
using Anatoli.DataAccess.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("PrivateLabelOwner")]
        public virtual Guid PrivateLabelOwner_Id { get; set; }

        public virtual Principal AddedBy { get; set; }
        public virtual Principal LastModifiedBy { get; set; }
        public virtual Principal PrivateLabelOwner { get; set; }
    }
}