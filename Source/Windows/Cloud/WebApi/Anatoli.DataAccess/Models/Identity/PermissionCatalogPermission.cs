using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class PermissionCatalogPermission
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual PermissionCatalog PermissionCatalog { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
