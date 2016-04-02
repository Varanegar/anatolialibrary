using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models.Identity
{
    public class Permission
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("ApplicationModuleResource")]
        public Guid ApplicationModuleResourceId { get; set; }
        public virtual ApplicationModuleResource ApplicationModuleResource { get; set; }
        [ForeignKey("PermissionAction")]
        public Guid PermissionActionId { get; set; }
        public virtual PermissionAction PermissionAction { get; set; }

        public virtual ICollection<PrincipalPermission> PrincipalPermissions { get; set; }

    }
}
