using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class Role : IdentityRole
    {

        [ForeignKey("Application")]
        public Guid ApplicationId { get; set; }
        public virtual Application Application { get; set; }
    }
}
