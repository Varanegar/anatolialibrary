using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class Principal 
    {
        [Key]
        public Guid Id { get; set; }
        public virtual ICollection<PrincipalPermission> PrincipalPermissions { get; set; }

    }
}
