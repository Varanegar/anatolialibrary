using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models.Identity
{
    public class Application
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ApplicationOwner> ApplicationOwners { get; set; }
        public virtual ICollection<ApplicationModule> ApplicationModules { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
