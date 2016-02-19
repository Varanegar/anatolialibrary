using Anatoli.Rastak.Business.Proxy;
using Anatoli.Rastak.Business.Proxy.Interfaces;
using Anatoli.Rastak.DataAccess.DataAdapter;
using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.ViewModels.Base;
using Anatoli.Rastak.ViewModels.EVC;
using Anatoli.Rastak.ViewModels.Order;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Rastak.Business.Domain.PurchaseOrder
{
    public class RastakPurchaseOrderStatusHistoryDomain : RastakBusinessDomain<RastakSellStatusViewModel, PurchaseOrderStatusHistoryViewModel>, IRastakBusinessDomain<RastakSellStatusViewModel, PurchaseOrderStatusHistoryViewModel>
    {
        #region Ctors
        public RastakPurchaseOrderStatusHistoryDomain()
            : this(AnatoliProxy<RastakSellStatusViewModel, PurchaseOrderStatusHistoryViewModel>.Create())
        { }
        public RastakPurchaseOrderStatusHistoryDomain(IAnatoliProxy<RastakSellStatusViewModel, PurchaseOrderStatusHistoryViewModel> proxy)
        {
            Proxy = proxy;
        }
        #endregion

        #region Methods
        public List<PurchaseOrderStatusHistoryViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<PurchaseOrderStatusHistoryViewModel> GetAllByOrderId(string orderId, string centerId)
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
