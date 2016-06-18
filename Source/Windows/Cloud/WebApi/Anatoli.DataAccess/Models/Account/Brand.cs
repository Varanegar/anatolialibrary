using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models
{
    public class Brand : AnatoliBaseModel
    {
        [StringLength(100)]
        public string BrandName { get; set; }
        public virtual ICollection<Product> ProductBrands { get; set; }
    }
}
