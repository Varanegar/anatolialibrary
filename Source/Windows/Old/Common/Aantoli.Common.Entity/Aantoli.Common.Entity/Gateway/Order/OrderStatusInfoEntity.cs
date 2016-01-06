using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Order
{
    public class OrderStatusInfoEntity : BaseEntity
    {
        public Guid OrderStatusId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Comment { get; set; }
    }
}
