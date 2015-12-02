using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models.Identity
{
    public class Principal
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<PrincipalPermission> PrincipalPermissions { get; set; }
    }
}
