using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Manufacture
{
    public class ManufactureEntity : BaseEntity
    {
        public int ManufactureCode { get; set; }
        public string ManufactureName { get; set; }
    }
}
