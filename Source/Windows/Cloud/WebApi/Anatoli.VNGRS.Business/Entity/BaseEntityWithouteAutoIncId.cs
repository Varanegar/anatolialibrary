using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingMap.Service.Entity
{
    public class BaseEntityWithouteAutoIncId : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
