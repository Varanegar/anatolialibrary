namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class DistCompanyRegion : BaseModel
    {
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public bool isLeaf { get; set; }
        public Nullable<int> Priority { get; set; }
        public Guid? ParentId { get; set; }
        public virtual ICollection<DistCompanyRegion> DistCompanyRegions { get; set; }
        [ForeignKey("ParentId")]
        public virtual DistCompanyRegion Parent { get; set; }
        [ForeignKey("DistCompanyRegionLevelType")]
        public Guid DistCompanyRegionLevelTypeId { get; set; }
        public virtual DistCompanyRegionLevelType DistCompanyRegionLevelType { get; set; }
        [ForeignKey("DistCompanyCenter")]
        public Guid DistCompanyCenterId { get; set; }
        public virtual DistCompanyCenter DistCompanyCenter { get; set; }
    }
}
