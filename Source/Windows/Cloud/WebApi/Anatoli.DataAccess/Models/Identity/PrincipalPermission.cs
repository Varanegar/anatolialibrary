using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class PrincipalPermission : BaseModel
    {
        public bool Grant { get; set; }

        public virtual Principal Principal { get; set; }
        public virtual Permission Permission { get; set; }

        [ForeignKey("Permission")]
        public Guid Permission_Id { get; set; }
        [ForeignKey("Principal")]
        public Guid Principal_Id { get; set; }
    }
}
