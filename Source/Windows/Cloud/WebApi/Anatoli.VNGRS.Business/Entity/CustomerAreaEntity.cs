using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Service.Entity
{
    [Table("CustomerArea")]
    public class CustomerAreaEntity : BaseEntityAutoIncId
    {
        [Column("CustomerId")]
        public Guid CustomerEntityId { get; set; }
        public virtual CustomerEntity CustomerEntity { set; get; }


        [Column("AreaId")]
        public Guid AreaEntityId { get; set; }
        public virtual AreaEntity AreaEntity { set; get; }

    }
}
