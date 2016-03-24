namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AnatoliRegion
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsRemoved { get; set; }

        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }

        public Guid? AnatoliRegion2Id { get; set; }

        public virtual ICollection<AnatoliRegion> AnatoliRegion1 { get; set; }
        [ForeignKey("AnatoliRegion2Id")]
        public virtual AnatoliRegion AnatoliRegion2 { get; set; }
    }
}
