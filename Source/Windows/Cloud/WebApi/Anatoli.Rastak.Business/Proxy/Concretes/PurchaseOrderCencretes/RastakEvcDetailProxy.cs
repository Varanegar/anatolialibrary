using Anatoli.Rastak.Business.Proxy.Interfaces;
using Anatoli.Rastak.DataAccess.Helpers.Entity;
using Anatoli.Rastak.ViewModels.EVC;
using Anatoli.Rastak.ViewModels.Order;
using Anatoli.Rastak.ViewModels.StoreModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Rastak.Business.Proxy.Concretes.PurchaseOrder
{
    public class RastakEvcDetailProxy : AnatoliProxy<RastakEvcDetailViewModel, RastakSellDetailViewModel>, IAnatoliProxy<RastakEvcDetailViewModel, RastakSellDetailViewModel>
    {
        public override RastakSellDetailViewModel Convert(RastakEvcDetailViewModel data, RastakStoreConfigEntity storeConfig)
        {
            throw new NotImplementedException();
        }

        public override RastakEvcDetailViewModel ReverseConvert(RastakSellDetailViewModel data, RastakStoreConfigEntity storeConfig)
        {
            return new RastakEvcDetailViewModel()
            {
                AppUserId = storeConfig.AppUserId,
                ModifiedDate = DateTime.Now,

                ProductId = data.ProductId,
                Qty = data.RequestQty,
                DetailId = data.SellDetailId,
                IsPrize= data.IsPrize,
            };
        }
    }
}
