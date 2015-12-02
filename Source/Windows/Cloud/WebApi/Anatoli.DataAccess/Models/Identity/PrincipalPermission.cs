using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.DataAccess.Models.Identity
{
    public class PrincipalPermission : BaseModel
    {
        public bool Grant { get; set; }

        public virtual Principal Principal { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
