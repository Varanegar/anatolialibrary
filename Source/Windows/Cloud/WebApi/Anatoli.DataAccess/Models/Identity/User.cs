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
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion

        [StringLength(200)]
        public string FullName { get; set; }
        public override string UserName { get; set; }
        [StringLength(50)]
        public string UserNameStr { get; set; }
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

        [StringLength(200)]
        public virtual string ResetSMSCode { get; set; }
        [StringLength(200)]
        public virtual string ResetSMSPass { get; set; }
        public virtual Nullable<DateTime> ResetSMSRequestTime { get; set; }
        [ForeignKey("AnatoliContact")]
        public Guid? AnatoliContactId { get; set; }
        [ForeignKey("ApplicationOwner")]
        public Guid? ApplicationOwnerId { get; set; }
        public virtual ApplicationOwner ApplicationOwner { get; set; }
        [ForeignKey("DataOwner")]
        public Guid DataOwnerId { get; set; }
        public virtual DataOwner DataOwner { get; set; }
        public virtual AnatoliContact AnatoliContact { get; set; }

        [ForeignKey("Principal")]
        public Guid PrincipalId { get; set; }
        public virtual Principal Principal { get; set; }

    }
}
