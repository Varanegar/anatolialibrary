using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class ApplicationModuleResource
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("ApplicationModule")]
        public Guid ApplicationModuleId { get; set; }
        public virtual ApplicationModule ApplicationModule { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
