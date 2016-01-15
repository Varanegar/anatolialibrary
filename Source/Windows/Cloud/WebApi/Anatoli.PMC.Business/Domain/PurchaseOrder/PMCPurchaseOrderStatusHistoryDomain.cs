using Anatoli.PMC.Business.Proxy;
using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.DataAdapter;
using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.ViewModels.Base;
using Anatoli.PMC.ViewModels.EVC;
using Anatoli.PMC.ViewModels.Order;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Domain.PurchaseOrder
{
    public class PMCPurchaseOrderStatusHistoryDomain : PMCBusinessDomain<PMCSellStatusViewModel, PurchaseOrderStatusHistoryViewModel>, IPMCBusinessDomain<PMCSellStatusViewModel, PurchaseOrderStatusHistoryViewModel>
    {
        #region Ctors
        public PMCPurchaseOrderStatusHistoryDomain()
            : this(AnatoliProxy<PMCSellStatusViewModel, PurchaseOrderStatusHistoryViewModel>.Create())
        { }
        public PMCPurchaseOrderStatusHistoryDomain(IAnatoliProxy<PMCSellStatusViewModel, PurchaseOrderStatusHistoryViewModel> proxy)
        {
            Proxy = proxy;
        }
        #endregion

        #region Methods
        public List<PurchaseOrderStatusHistoryViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<PurchaseOrderStatusHistoryViewModel> GetAllByOrderId(string orderId, int centerId)
        {
            var orderStatus = SellAdapter.Instance.GetPurchaseOrderStatusByPOId(orderId, centerId);
            return orderStatus;
        }

        public List<PurchaseOrderStatusHistoryViewModel> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public PurchaseOrderStatusHistoryViewModel Publish(PurchaseOrderStatusHistoryViewModel baseViewModels)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
