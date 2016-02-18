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
    public class RastakEvcProxy : AnatoliProxy<RastakEvcViewModel, RastakSellViewModel>, IAnatoliProxy<RastakEvcViewModel, RastakSellViewModel>
    {
        public IAnatoliProxy<RastakEvcDetailViewModel, RastakSellDetailViewModel> RastakEvcDetailProxy { get; set; }
        #region Ctors
        public RastakEvcProxy() :
            this(AnatoliProxy<RastakEvcDetailViewModel, RastakSellDetailViewModel>.Create()
            )
        { }

        public RastakEvcProxy(IAnatoliProxy<RastakEvcDetailViewModel, RastakSellDetailViewModel> evcDetailProxy
            )
        {
            RastakEvcDetailProxy = evcDetailProxy;
        }
        #endregion

        public override RastakSellViewModel Convert(RastakEvcViewModel data, RastakStoreConfigEntity storeConfig)
        {
            throw new NotImplementedException();
        }

        public override RastakEvcViewModel ReverseConvert(RastakSellViewModel data, RastakStoreConfigEntity storeConfig)
        {
            return new RastakEvcViewModel()
            {
                AppUserId = storeConfig.AppUserId,
                ModifiedDate = DateTime.Now,

                CustomerId = data.CustomerId,
                CenterId = data.CenterId,
                //SalesmanId = data.SalesmanId,
                DateOf = data.InvoiceDateTime,
                EVCDetail = RastakEvcDetailProxy.ReverseConvert(data.SellDetail, storeConfig),
            };
        }
    }
}
