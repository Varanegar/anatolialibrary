using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.DataAccess.Helpers.Entity;
using Anatoli.PMC.ViewModels.Base;
using Anatoli.PMC.ViewModels.EVC;
using Anatoli.PMC.ViewModels.Order;
using Anatoli.PMC.ViewModels.StoreModels;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Proxy.Concretes.Customer
{
    public class PMCCustomerProxy : AnatoliProxy<PMCCustomerViewModel, CustomerViewModel>, IAnatoliProxy<PMCCustomerViewModel, CustomerViewModel>
    {
        public override CustomerViewModel Convert(PMCCustomerViewModel data, PMCStoreConfigEntity storeConfig)
        {
            throw new NotImplementedException();
        }

        public override PMCCustomerViewModel ReverseConvert(CustomerViewModel data, PMCStoreConfigEntity storeConfig)
        {
            var customer = new PMCCustomerViewModel()
            {
                AppUserId = storeConfig.AppUserId,
                ModifiedDate = DateTime.Now,

                Address = data.Address,
                BirthDay = data.BirthDay,
                CustomerCode = data.CustomerCode,
                CustomerName = data.CustomerName,
                CustomerSiteUserId = data.UniqueId.ToString(),
                Email = data.Email,
                Mobile = data.Mobile,
                NationalCode = data.NationalCode,
                Phone = data.Phone,
                PostCode = data.PostalCode,
                CustomerGroupId = storeConfig.CustomerGroupId,
                CustomerTypeId = storeConfig.CustomerTypeId,
                InitialBalanceTypeId = 1,
                InitialAmount = 0,
                IsBlackList = false,    
            };
            customer.DeclareDate = GeneralCommands.GetServerShamsiDateTime(null);
            return customer;
        }
    }
}
