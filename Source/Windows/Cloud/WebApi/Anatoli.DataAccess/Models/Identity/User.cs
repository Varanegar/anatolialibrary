using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Anatoli.DataAccess.Models.Identity
{
    public class User : BaseModel, IUser<Guid>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime LastEntry { get; set; }
        public string LastEntryIp { get; set; }

        public virtual Principal Principal { get; set; }
        public virtual Role Role { get; set; }
        public virtual Group Group { get; set; }
    } 
}
