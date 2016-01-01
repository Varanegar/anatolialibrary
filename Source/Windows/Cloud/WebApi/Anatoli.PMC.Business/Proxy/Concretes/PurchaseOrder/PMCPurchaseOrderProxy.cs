using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.ViewModels.Order;
using Anatoli.PMC.ViewModels.StoreModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Proxy.Concretes.PurchaseOrder
{
    public class PMCPurchaseOrderProxy : AnatoliProxy<PMCSellViewModel, PurchaseOrderViewModel>, IAnatoliProxy<PMCSellViewModel, PurchaseOrderViewModel>
    {       
        public IAnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel> PMCSellDetailProxy { get; set; }
        #region Ctors
        public PMCPurchaseOrderProxy() :
            this(AnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel>.Create()
            )
        { }

        public PMCPurchaseOrderProxy(IAnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel> sellDetailProxy
            )
        {
            PMCSellDetailProxy = sellDetailProxy;
        }
        #endregion

        public override PurchaseOrderViewModel Convert(PMCSellViewModel data)
        {
            return new PurchaseOrderViewModel()
            {
                LineItems = PMCSellDetailProxy.Convert(data.SellDetail),
            };
        }

        public override PMCSellViewModel ReverseConvert(PurchaseOrderViewModel data)
        {
            return new PMCSellViewModel()
            {
                SellDetail = PMCSellDetailProxy.ReverseConvert(data.LineItems),
            };
        }
    }
}
