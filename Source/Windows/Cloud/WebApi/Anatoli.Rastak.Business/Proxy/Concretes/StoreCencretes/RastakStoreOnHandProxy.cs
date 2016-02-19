using Anatoli.Rastak.Business.Proxy.Interfaces;
using Anatoli.Rastak.DataAccess.Helpers.Entity;
using Anatoli.Rastak.ViewModels.StoreModels;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Rastak.Business.Proxy.Concretes.StoreCencretes
{
    public class RastakStoreOnHandProxy : AnatoliProxy<RastakStoreOnhandViewModel, StoreActiveOnhandViewModel>, IAnatoliProxy<RastakStoreOnhandViewModel, StoreActiveOnhandViewModel>
    {
        public override StoreActiveOnhandViewModel Convert(RastakStoreOnhandViewModel data, RastakStoreConfigEntity storeConfig)
        {
            return new StoreActiveOnhandViewModel()
            {
                ProductGuid = data.ProductGuid,
                StoreGuid = data.StoreGuid,
                Qty = data.Qty,
            };
        }

        public override RastakStoreOnhandViewModel ReverseConvert(StoreActiveOnhandViewModel data, RastakStoreConfigEntity storeConfig)
        {
            return new RastakStoreOnhandViewModel()
            {
                ProductGuid = data.ProductGuid,
                StoreGuid = data.StoreGuid,
                Qty = data.Qty,
            };
        }
    }
}
