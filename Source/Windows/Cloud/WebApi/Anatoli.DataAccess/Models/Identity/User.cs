using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class User : IdentityUser
    {
        #region BaseModel
        [Key]
        public override string Id { get; set; }
        public int Number_ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsRemoved { get; set; }

        public virtual Principal AddedBy { get; set; }
        public virtual Principal LastModifiedBy { get; set; }
        [ForeignKey("PrivateLabelOwner")]
        public virtual Guid PrivateLabelOwner_Id { get; set; }
        
        public virtual Principal PrivateLabelOwner { get; set; }
        #endregion

        [StringLength(200)]
        public string FullName { get; set; }
        public override string UserName { get; set; }
        public string Password { get; set; }
        public override string Email
        {
            get { return base.Email; }
            set
            {
                base.Email = value;
            }
        }
        public override string PhoneNumber { get; set; }
        public DateTime LastEntry { get; set; }
        public string LastEntryIp { get; set; }

        [ForeignKey("Principal")]
        public virtual Guid Principal_Id { get; set; }
        public virtual Principal Principal { get; set; }
        public virtual Role Role { get; set; }
        public virtual Group Group { get; set; }
        [StringLength(200)]
        public virtual string ResetSMSCode { get; set; }
        [StringLength(200)]
        public virtual string ResetSMSPass { get; set; }
        public virtual Nullable<DateTime> ResetSMSRequestTime { get; set; }


        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
