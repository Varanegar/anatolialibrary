using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingMap.Common.ViewModel;

namespace TrackingMap.Service.Entity
{
    [Table("AreaPoint")]
    public class AreaPointEntity : BaseEntityAutoIncId
    {
        [Column("AreaId")]
        public Guid AreaEntityId { get; set; }
        public virtual AreaEntity AreaEntity { set; get; }

        [Column("CustomerId")]
        public Guid? CustomerEntityId { get; set; }

        [Column("Priority")]
        public int Priority { get; set; }

        [Column("Latitude")]
        public double Latitude { set; get; }

        [Column("Longitude")]
        public double Longitude { set; get; }

        public AreaPointEntity()
        {
        }

        public AreaPointEntity(AreaPointView view)
        {
            this.Latitude = view.Lat;
            this.Longitude = view.Lng;
            this.Priority = view.Pr;
            this.AreaEntityId = view.AreaId;
            this.CustomerEntityId = view.CstId;
        }

    }
}
