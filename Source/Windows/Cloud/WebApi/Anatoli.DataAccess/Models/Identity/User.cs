using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

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
        public virtual Principal PrivateLabelOwner { get; set; }
        #endregion

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

        public virtual Principal Principal { get; set; }
        public virtual Role Role { get; set; }
        public virtual Group Group { get; set; }
    }
}
