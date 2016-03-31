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
        public virtual ICollection<ApplicationModuleResource> Childs { get; set; }
        [ForeignKey("ParentId")]
        public virtual ApplicationModuleResource Parent { get; set; }

        public Guid? ParentId { get; set; }
        public int NodeId { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }

    }
}
