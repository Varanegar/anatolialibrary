using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.Helpers.Entity;
using Anatoli.PMC.ViewModels.EVC;
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
    public class PMCEvcDetailPMCSellDetailViewModelProxy : AnatoliProxy<PMCEvcDetailViewModel, PMCSellDetailViewModel>, IAnatoliProxy<PMCEvcDetailViewModel, PMCSellDetailViewModel>
    {
        public override PMCSellDetailViewModel Convert(PMCEvcDetailViewModel data, PMCStoreConfigEntity storeConfig)
        {
            throw new NotImplementedException();
        }

        public override PMCEvcDetailViewModel ReverseConvert(PMCSellDetailViewModel data, PMCStoreConfigEntity storeConfig)
        {
            return new PMCEvcDetailViewModel()
            {
                ProductId = data.ProductId,
                Qty = data.RequestQty,
                DetailId = data.SellDetailId,
                IsPrize= data.IsPrize,
            };
        }
    }
}
