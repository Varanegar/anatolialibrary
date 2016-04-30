using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Service.Entity
{
    [Table("VisitorDailyPath")]
    public class VisitorDailyPathEntity : BaseEntityAutoIncId
    {
        [Column("VisitorId")]
        public Guid VisitorEntityId { get; set; }
        public virtual VisitorEntity VisitorEntity { set; get; }

        [Column("VisitorPathId")]
        public Guid AreaEntityId { get; set; }
        public virtual AreaEntity AreaEntity { set; get; }

        [Column("Date", TypeName = "varchar")]
        [MaxLength(10)]
        public string Date { get; set; }


    }
}
