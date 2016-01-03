using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.ViewModels.Base
{
    public class PMCCustomerViewModel : PMCBaseViewModel
    {
        public long? CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string NationalCode { get; set; }
        public string CustomerSiteUserId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerGroupId { get; set; }
        public int CustomerTypeId { get; set; }
        public int InitialAmount  { get; set; }
        public int InitialBalanceTypeId  { get; set; }
        public string DeclareDate  { get; set; }
        public bool IsBlackList { get; set; }
    }
}
