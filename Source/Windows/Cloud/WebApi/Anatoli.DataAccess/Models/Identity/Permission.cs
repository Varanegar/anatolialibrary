using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.DataAccess.Models.Identity
{
    public class Permission : BaseModel
    {
        public string PersianTitle { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }

        public Permission Parent { get; set; }
        public virtual ICollection<PrincipalPermission> PrincipalPermissions { get; set; }
    }
}
