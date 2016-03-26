using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models
{

    public class AnatoliPlace
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsRemoved { get; set; }

        public string Phone { get; set; }
        [StringLength(20)]
        public string Mobile { get; set; }
        [StringLength(500)]
        public string Email { get; set; }
        [StringLength(500)]
        public string MainStreet { get; set; }
        [StringLength(500)]
        public string OtherStreet { get; set; }
        [StringLength(20)]
        public string PostalCode { get; set; }
        [StringLength(20)]
        public string NationalCode { get; set; }
        [ForeignKey("RegionInfo"), Column(Order = 0)]
        public Nullable<Guid> RegionInfoId { get; set; }
        [ForeignKey("RegionLevel1"), Column(Order = 1)]
        public Nullable<Guid> RegionLevel1Id { get; set; }
        [ForeignKey("RegionLevel2"), Column(Order = 2)]
        public Nullable<Guid> RegionLevel2Id { get; set; }
        [ForeignKey("RegionLevel3"), Column(Order = 3)]
        public Nullable<Guid> RegionLevel3Id { get; set; }
        [ForeignKey("RegionLevel4"), Column(Order = 4)]
        public Nullable<Guid> RegionLevel4Id { get; set; }

        public virtual AnatoliRegion RegionInfo { get; set; }
        public virtual AnatoliRegion RegionLevel1 { get; set; }
        public virtual AnatoliRegion RegionLevel2 { get; set; }
        public virtual AnatoliRegion RegionLevel3 { get; set; }
        public virtual AnatoliRegion RegionLevel4 { get; set; }

        public virtual ICollection<AnatoliAccount> AnatoliAccounts { get; set; }


    }
}
