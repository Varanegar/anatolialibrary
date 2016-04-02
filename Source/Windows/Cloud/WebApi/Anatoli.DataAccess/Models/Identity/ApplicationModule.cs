using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class ApplicationModule
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Application")]
        public Guid ApplicationId { get; set; }
        public virtual Application Application { get; set; }
        public virtual ICollection<ApplicationModuleResource> ApplicationModuleResources { get; set; }

    }
}
