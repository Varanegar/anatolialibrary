using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Anatoli.DataAccess.Models
{
    public class ProductType : BaseModel
    {
        [StringLength(100)]
        public string ProductTypeName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<StockProductRequest> StockProductRequests { get; set; }
        public virtual ICollection<StockProductRequestRule> StockProductRequestRules { get; set; }
    }
}
