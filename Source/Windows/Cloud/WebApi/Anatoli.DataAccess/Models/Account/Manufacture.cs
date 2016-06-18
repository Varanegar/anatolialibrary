using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models
{
    public class Manufacture : AnatoliBaseModel
    {
        [StringLength(100)]
        public string ManufactureName { get; set; }
        public virtual ICollection<Product> ProductManufactures { get; set; }
    }
}
