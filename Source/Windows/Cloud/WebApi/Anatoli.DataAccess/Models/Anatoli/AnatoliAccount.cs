using Anatoli.DataAccess.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models
{

    public class AnatoliAccount
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsRemoved { get; set; }

        [ForeignKey("AnatoliPlace")]
        public Nullable<Guid> AnatoliPlaceId { get; set; }
        [ForeignKey("AnatoliContact")]
        public Nullable<Guid> AnatoliContactId { get; set; }
        public virtual AnatoliContact AnatoliContact { get; set; }

        public virtual AnatoliPlace AnatoliPlace { get; set; }


    }
}
