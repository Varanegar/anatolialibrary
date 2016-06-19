﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class ReorderCalcType : AnatoliBaseModel
    {
        [StringLength(100)]
        public string ReorderTypeName { get; set; }
        public virtual ICollection<StockProduct> StockProducts { get; set; }
        public virtual ICollection<StockProductRequestRule> StockProductRequestRules { get; set; }
    }
}
