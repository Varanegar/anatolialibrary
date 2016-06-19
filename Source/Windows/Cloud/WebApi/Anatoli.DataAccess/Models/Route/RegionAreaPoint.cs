using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models.Route
{
    public class RegionAreaPoint : AnatoliBaseModel
    {
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public int Priority { set; get; }
        [ForeignKey("RegionArea")]
        public Guid RegionAreaId { get; set; }
        public virtual RegionArea RegionArea { get; set; }
    }
}
