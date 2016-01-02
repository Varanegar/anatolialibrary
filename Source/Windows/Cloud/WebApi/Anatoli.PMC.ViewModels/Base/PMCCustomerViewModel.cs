using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.ViewModels.Base
{
    public class PMCCustomerViewModel : PMCBaseViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerSiteUserId { get; set; }
    }
}
