using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DataAccess.Models
{
    public class Company : BaseModel
    {
        public int CompanyCode { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<DistCompanyCenter> DistCompanyCenters { get; set; }
        public virtual ICollection<CustomerNotVerified> CustomerNotVerifieds { get; set; }

    }
}
