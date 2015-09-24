using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Product
{
    public class ProductCharValueEntity : BaseEntity
    {
        public string CharValueText { get; set; }
        public decimal CharValueFromAmount { get; set; }
        public decimal CharValueToAmount { get; set; }
    }
}
