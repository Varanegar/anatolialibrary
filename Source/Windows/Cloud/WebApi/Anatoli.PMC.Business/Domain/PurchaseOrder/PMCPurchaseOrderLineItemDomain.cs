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
    public class PMCPurchaseOrderLineItemDomain : PMCBusinessDomain<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel>, IPMCBusinessDomain<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel>
    {
        #region Ctors
        public PMCPurchaseOrderLineItemDomain()
            : this(AnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel>.Create())
        { }
        public PMCPurchaseOrderLineItemDomain(IAnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel> proxy)
        {
            Proxy = proxy;
        }
        #endregion

        #region Methods
        public List<PurchaseOrderLineItemViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<PurchaseOrderLineItemViewModel> GetAllByOrderId(string orderId, int centerId)
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
