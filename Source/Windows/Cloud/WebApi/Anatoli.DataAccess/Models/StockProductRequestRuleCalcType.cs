using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class StockProductRequestRuleCalcType : BaseModel
    {
        [StringLength(100)]
        public string StockProductRequestRuleCalcTypeName { get; set; }
        public virtual ICollection<StockProductRequestRule> StockProductRequestRules { get; set; }
    }
}
