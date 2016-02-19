using Anatoli.Rastak.Business.Proxy.Interfaces;
using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.DataAccess.Helpers.Entity;
using Anatoli.Rastak.ViewModels.Base;
using Anatoli.Rastak.ViewModels.EVC;
using Anatoli.Rastak.ViewModels.Order;
using Anatoli.Rastak.ViewModels.StoreModels;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Rastak.Business.Proxy.Concretes.Customer
{
    public class RastakCustomerProxy : AnatoliProxy<RastakCustomerViewModel, CustomerViewModel>, IAnatoliProxy<RastakCustomerViewModel, CustomerViewModel>
    {
        public override CustomerViewModel Convert(RastakCustomerViewModel data, RastakStoreConfigEntity storeConfig)
        {
            throw new NotImplementedException();
        }

        public override RastakCustomerViewModel ReverseConvert(CustomerViewModel data, RastakStoreConfigEntity storeConfig)
        {
            var customer = new RastakCustomerViewModel()
            {
                AppUserId = storeConfig.AppUserId,
                ModifiedDate = DateTime.Now,

                Address = data.MainStreet + ' ' + data.OtherStreet,
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
