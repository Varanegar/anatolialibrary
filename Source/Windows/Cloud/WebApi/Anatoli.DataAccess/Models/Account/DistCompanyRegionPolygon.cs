namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DistCompanyRegionPolygon : BaseModel
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        [ForeignKey("DistCompanyRegion")]
        public Guid DistCompanyRegionId { get; set; }
        public virtual DistCompanyRegion DistCompanyRegion { get; set; }
    }
}
