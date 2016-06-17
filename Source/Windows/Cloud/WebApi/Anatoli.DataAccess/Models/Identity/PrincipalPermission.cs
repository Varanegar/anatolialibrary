using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class PrincipalPermission //: BaseModelAnatoli
    {
        [Key]
        public Guid Id { get; set; }
        public int Grant { get; set; }

        public virtual Principal Principal { get; set; }
        public virtual Permission Permission { get; set; }

        [ForeignKey("Permission")]
        public Guid Permission_Id { get; set; }
        [ForeignKey("Principal")]
        public Guid PrincipalId { get; set; }
    }    
}
