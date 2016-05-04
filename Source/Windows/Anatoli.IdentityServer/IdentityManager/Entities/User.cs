using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anatoli.IdentityServer.Entities
{
    public class User : IdentityUser
    {
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }

        public virtual ICollection<UserDeviceToken> UserDeviceTokens { get; set; }
        public virtual ICollection<Channel> Channels
        {
            get;
            set;
        }
    }
}