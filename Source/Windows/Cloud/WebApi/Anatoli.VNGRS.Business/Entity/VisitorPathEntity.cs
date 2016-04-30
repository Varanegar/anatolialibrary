using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Service.Entity
{
    [Table("VisitorPath")]
    public class VisitorPathEntity : BaseEntityWithouteAutoIncId
    {
        [Column("VisitorId")]
        public Guid VisitorEntityId { get; set; }
        public virtual VisitorEntity VisitorEntity { set; get; }

        [Column("Latitude")]
        public double Latitude { set; get; }

        [Column("Longitude")]
        public double Longitude { set; get; }

        public DateTime Date { get; set; }

    }
}
