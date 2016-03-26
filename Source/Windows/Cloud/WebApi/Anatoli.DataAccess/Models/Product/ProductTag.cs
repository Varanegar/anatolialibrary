using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anatoli.DataAccess.Models
{
    public class ProductTag : BaseModel
    {
        [StringLength(100)]
        public string ProductTagName { get; set; }
        public virtual ICollection<ProductTagValue> ProductTagValues { get; set; }
    }
}