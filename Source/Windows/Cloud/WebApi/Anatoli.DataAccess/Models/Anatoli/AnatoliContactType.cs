using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models.Identity
{
    public class AnatoliContactType
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<AnatoliContact> AnatoliContacts { get; set; }
    }
}
