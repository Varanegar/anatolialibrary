using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.DataAccess.Models
{
    public class Manufacture : BaseModel
    {
        public string ManufactureName { get; set; }
        public virtual ICollection<Product> ProductManufactures { get; set; }
    }
}
