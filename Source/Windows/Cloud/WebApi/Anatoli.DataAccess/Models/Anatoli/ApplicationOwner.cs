using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models.Identity
{
    public class ApplicationOwner
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(200)]
        public string WebHookURI { get; set; }
        [StringLength(200)]
        public string WebHookUsername { get; set; }
        [StringLength(200)]
        public string WebHookPassword { get; set; }
        [ForeignKey("AnatoliContact")]
        public Guid AnatoliContactId { get; set; }
        [ForeignKey("Application")]
        public Guid ApplicationId { get; set; }
        public virtual Application Application { get; set; }
        public virtual AnatoliContact AnatoliContact { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
