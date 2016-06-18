using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.DataAccess.Models
{
    public class ProductTagValue : AnatoliBaseModel
    {
        public DateTime? FromDate { get; set; }
        public string FromPDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ToPDate { get; set; }
        [ForeignKey("ProductTag")]
        public Guid ProductTagId { get; set; }
        public virtual ProductTag ProductTag { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
