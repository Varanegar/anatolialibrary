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
    public class RastakPurchaseOrderLineItemDomain : RastakBusinessDomain<RastakSellDetailViewModel, PurchaseOrderLineItemViewModel>, IRastakBusinessDomain<RastakSellDetailViewModel, PurchaseOrderLineItemViewModel>
    {
        #region Ctors
        public RastakPurchaseOrderLineItemDomain()
            : this(AnatoliProxy<RastakSellDetailViewModel, PurchaseOrderLineItemViewModel>.Create())
        { }
        public RastakPurchaseOrderLineItemDomain(IAnatoliProxy<RastakSellDetailViewModel, PurchaseOrderLineItemViewModel> proxy)
        {
            Proxy = proxy;
        }
        #endregion

        #region Methods
        public List<PurchaseOrderLineItemViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<PurchaseOrderLineItemViewModel> GetAllByOrderId(string orderId, string centerId)
        {
            var orderLineItems = SellAdapter.Instance.GetPurchaseOrderLineItemsByPOId(orderId, centerId);
            return orderLineItems;
        }

        public List<PurchaseOrderLineItemViewModel> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public PurchaseOrderLineItemViewModel Publish(PurchaseOrderLineItemViewModel baseViewModels)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
