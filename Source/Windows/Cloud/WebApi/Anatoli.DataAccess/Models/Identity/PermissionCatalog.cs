using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class PermissionCatalog
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("PermissionCatalougeParent")]
        public virtual Guid? PermissionCatalougeParentId { get; set; }

        public virtual PermissionCatalog PermissionCatalougeParent { get; set; }
        public virtual ICollection<PermissionCatalogPermission> PermissionCatalogPermissions { get; set; }
        public virtual ICollection<PrincipalPermissionCatalog> PrincipalPermissionCatalogs { get; set; }
    }
}
