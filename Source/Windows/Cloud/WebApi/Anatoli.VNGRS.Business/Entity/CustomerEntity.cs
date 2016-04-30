using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Service.Entity
{
    [Table("Customer")]
    public class CustomerEntity : BaseEntityWithouteAutoIncId
    {

        [Column("Title", TypeName = "varchar")]
        [MaxLength(200)]
        public string Title { set; get; }

        [Column("Code", TypeName = "varchar")]
        [MaxLength(20)]
        public string Code { set; get; }

        [Column("ShopTitle", TypeName = "varchar")]
        [MaxLength(200)]
        public string ShopTitle { set; get; }

        [Column("Phone", TypeName = "varchar")]
        [MaxLength(200)]
        public string Phone { set; get; }

        [Column("Address", TypeName = "varchar")]
        [MaxLength(500)]
        public string Address { set; get; }

        [Column("Activity", TypeName = "varchar")]
        [MaxLength(100)]
        public string Activity { set; get; }

        [Column("Desc", TypeName = "varchar(MAX)")]
        public string Desc { set; get; }

        [Column("Latitude")]
        public double? Latitude { set; get; }

        [Column("Longitude")]
        public double? Longitude { set; get; }

    }
}
