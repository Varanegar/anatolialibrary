using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingMap.Common.Enum;

namespace TrackingMap.Service.Entity
{
    [Table("Transaction")]
    public class TransactionEntity : BaseEntityWithouteAutoIncId
    {
        [Column("VisitorId")]
        public Guid VisitorEntityId { get; set; }
        public virtual VisitorEntity VisitorEntity { set; get; }


        [Column("CustomerId")]
        public Guid? CustomerEntityId { get; set; }
        public virtual CustomerEntity CustomerEntity { set; get; }

        [Column("TransactionType", TypeName = "int")]
        public PointType TransactionType { get; set; }

        [Column("CustomerType", TypeName = "int")]
        public ESubType CustomerType { get; set; }

        [Column("Latitude")]
        public double Latitude { set; get; }

        [Column("Longitude")]
        public double Longitude { set; get; }

        [Column("Desc", TypeName = "varchar(MAX)")]
        public string Desc { set; get; }

        public DateTime DateTime { set; get; }

    }
}
