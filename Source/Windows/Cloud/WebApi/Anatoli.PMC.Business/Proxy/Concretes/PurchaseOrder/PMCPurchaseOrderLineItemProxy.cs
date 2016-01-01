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
    public class PMCPurchaseOrderLineItem : AnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel>, IAnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel>
    {
        public override PurchaseOrderLineItemViewModel Convert(PMCSellDetailViewModel data)
        {
            return new PurchaseOrderLineItemViewModel()
            {
            };
        }

        public override PMCSellDetailViewModel ReverseConvert(PurchaseOrderLineItemViewModel data)
        {
            return new PMCSellDetailViewModel()
            {
            };
        }
    }
}
