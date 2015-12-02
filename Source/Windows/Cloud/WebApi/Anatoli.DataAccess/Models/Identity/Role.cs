using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.DataAccess.Models.Identity
{
    public class Role : BaseModel
    {
        public string Name { get; set; }

        public virtual Principal Principal { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
