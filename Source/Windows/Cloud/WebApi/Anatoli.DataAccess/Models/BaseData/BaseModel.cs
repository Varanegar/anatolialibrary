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

        [ForeignKey("ApplicationOwner")]
        public virtual Guid ApplicationOwnerId { get; set; }
        [ForeignKey("DataOwner")]
        public virtual Guid DataOwnerId { get; set; }
        [ForeignKey("DataOwnerCenter")]
        public virtual Guid DataOwnerCenterId { get; set; }
        public virtual DataOwnerCenter DataOwnerCenter { get; set; }
        public virtual DataOwner DataOwner { get; set; }
        public virtual ApplicationOwner ApplicationOwner { get; set; }
        public virtual User AddedBy { get; set; }
        public virtual User LastModifiedBy { get; set; }
    }
}