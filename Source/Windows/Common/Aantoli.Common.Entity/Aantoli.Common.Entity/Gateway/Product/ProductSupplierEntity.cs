using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Product
{
    public class ProductSupplierEntity : BaseEntity
    {
        public Guid SupplierId { get; set; }
        public string SupplierComment { get; set; }
    }
}
