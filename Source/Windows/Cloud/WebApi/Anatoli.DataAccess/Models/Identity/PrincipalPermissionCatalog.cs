using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class PrincipalPermissionCatalog //: BaseModelAnatoli
    {
        [Key]
        public Guid Id { get; set; }
        public int Grant { get; set; }

        public virtual Principal Principal { get; set; }
        public virtual PermissionCatalog PermissionCatalog { get; set; }

        [ForeignKey("PermissionCatalog")]
        public Guid PermissionCatalog_Id { get; set; }
        [ForeignKey("Principal")]
        public Guid PrincipalId { get; set; }
    }
}
