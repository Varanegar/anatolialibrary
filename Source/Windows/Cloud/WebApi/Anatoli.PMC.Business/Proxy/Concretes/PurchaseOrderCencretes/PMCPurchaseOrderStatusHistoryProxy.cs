using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.DataAdapter;
using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.DataAccess.Helpers.Entity;
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
    public class PMCPurchaseOrderStatusHistoryProxy : AnatoliProxy<PMCSellStatusViewModel, PurchaseOrderStatusHistoryViewModel>, IAnatoliProxy<PMCSellStatusViewModel, PurchaseOrderStatusHistoryViewModel>
    {       
        public override PurchaseOrderStatusHistoryViewModel Convert(PMCSellStatusViewModel data, PMCStoreConfigEntity storeConfig)
        {
            return new PurchaseOrderStatusHistoryViewModel()
            {
            };
        }

        public override PMCSellStatusViewModel ReverseConvert(PurchaseOrderStatusHistoryViewModel data, PMCStoreConfigEntity storeConfig)
        {
            return new PMCSellStatusViewModel()
            {

            };
        }
    }
}
