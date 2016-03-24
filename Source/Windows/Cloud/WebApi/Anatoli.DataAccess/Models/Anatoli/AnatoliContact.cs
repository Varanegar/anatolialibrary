using Anatoli.DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models
{
    public class AnatoliContact
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsRemoved { get; set; }

        [StringLength(200)]
        public string ContactName { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        public Nullable<DateTime> BirthDay { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(20)]
        public string Mobile { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Website { get; set; }
        [StringLength(20)]
        public string NationalCode { get; set; }

        public virtual ICollection<AnatoliAccount> AnatoliAccounts { get; set; }
        public virtual ICollection<User> Users { get; set; }
        [ForeignKey("AnatoliContactType")]
        public Guid AnatoliContactTypeId { get; set; }
        public virtual AnatoliContactType AnatoliContactType { get; set; }

    }
}
