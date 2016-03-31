using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models.Identity
{
    public class Group 
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }

        public virtual Principal Manager { get; set; }
        public virtual ICollection<Principal> Principals { get; set; }
        public virtual ICollection<Group> Childs { get; set; }
        [ForeignKey("ParentId")]
        public virtual Group Parent { get; set; }

        public Guid? ParentId { get; set; }
        public int NodeId { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }
        public virtual Principal Principal { get; set; }

    }
}
