using System;
using System.Collections.Generic;

namespace Anatoli.IdentityServer.Entities
{
    public class Channel
    {
        public Guid Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public virtual ICollection<User> Users
        {
            get;
            set;
        }
    }
}