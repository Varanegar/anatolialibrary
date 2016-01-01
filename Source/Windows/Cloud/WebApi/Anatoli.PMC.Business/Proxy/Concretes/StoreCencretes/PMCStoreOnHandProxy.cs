using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.ViewModels.StoreModels;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Proxy.Concretes.StoreCencretes
{
    public class PMCStoreOnHandProxy : AnatoliProxy<PMCStoreOnhandViewModel, StoreActiveOnhandViewModel>, IAnatoliProxy<PMCStoreOnhandViewModel, StoreActiveOnhandViewModel>
    {
        public override StoreActiveOnhandViewModel Convert(PMCStoreOnhandViewModel data)
        {
            return new StoreActiveOnhandViewModel()
            {
                ProductGuid = data.ProductGuid,
                StoreGuid = data.StoreGuid,
                Qty = data.Qty,
            };
        }

        public override PMCStoreOnhandViewModel ReverseConvert(StoreActiveOnhandViewModel data)
        {
            return new PMCStoreOnhandViewModel()
            {
                ProductGuid = data.ProductGuid,
                StoreGuid = data.StoreGuid,
                Qty = data.Qty,
            };
        }
    }
}
