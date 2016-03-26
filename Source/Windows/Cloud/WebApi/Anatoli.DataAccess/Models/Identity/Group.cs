using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.DataAccess.Models.Identity
{
    public class Group : BaseModel
    {
        public string Name { get; set; }

        public virtual ApplicationOwner Manager { get; set; }
        public virtual ApplicationOwner Principal { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
