using Anatoli.DMC.Business.Proxy.Interfaces;
using Anatoli.DMC.DataAccess.DataAdapter;
using Anatoli.DMC.DataAccess.Helpers;
using Anatoli.DMC.ViewModels.Base;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.Business.Domain.PurchaseOrder
{
    public class DMCPurchaseOrderDomain : DMCBusinessDomain<DMCCustomerViewModel, CustomerViewModel>, IDMCBusinessDomain<DMCCustomerViewModel, CustomerViewModel>
    {

        #region Ctors
        public DMCPurchaseOrderDomain()
        { }
        #endregion

        #region Methods
        public int GetAllByCustomerId(Guid customerId, string statusId, string centerId)
        {
            if (centerId.ToLower() == "all")
                return DMCCustomerAdapter.Instance.GetCustomerId(customerId.ToString());
            else
                return -1;
        }

        public List<DMCCustomerViewModel> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public CustomerViewModel Publish(CustomerViewModel baseViewModels)
        {
            var data = Mapper.Map<DMCCustomerViewModel>(baseViewModels);

            return baseViewModels;
        }
        #endregion
    }
}
