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

        [StringLength(200)]
        public string Title { get; set; }
        public virtual ICollection<PrincipalPermission> PrincipalPermissions { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual Group Group { get; set; }
        [ForeignKey("ApplicationOwner")]
        public Guid ApplicationOwnerId { get; set; }
        public virtual ApplicationOwner ApplicationOwner { get; set; }
        public ICollection<PrincipalPermissionCatalog> PrincipalPermissionCatalogs { get; set; }
    }
}
