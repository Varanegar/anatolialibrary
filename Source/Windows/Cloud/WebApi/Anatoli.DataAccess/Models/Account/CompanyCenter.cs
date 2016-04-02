using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class CompanyCenter : BaseModel
    {
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Nullable<int> Priority { get; set; }

        public int CenterCode { get; set; }
        [StringLength(100)]
        public string CenterName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(200)]
        public string Phone { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public Guid CenterTypeId { get; set; }
        public Guid? ParentId { get; set; }
        public bool SupportAppOrder { get; set; }
        public bool SupportWebOrder { get; set; }
        public bool SupportCallCenterOrder { get; set; }
        public virtual ICollection<CompanyCenter> CompanyCenters { get; set; }
        public virtual ICollection<Stock> DistCenterStocks { get; set; }

        [ForeignKey("ParentId")]
        public virtual CompanyCenter Parent { get; set; }

        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
