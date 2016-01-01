using Anatoli.PMC.Business.Proxy.Interfaces;
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
    public class PMCEvcProxy : AnatoliProxy<PMCEvcViewModel, PMCSellViewModel>, IAnatoliProxy<PMCEvcViewModel, PMCSellViewModel>
    {
        public IAnatoliProxy<PMCEvcDetailViewModel, PMCSellDetailViewModel> PMCEvcDetailProxy { get; set; }
        #region Ctors
        public PMCEvcProxy() :
            this(AnatoliProxy<PMCEvcDetailViewModel, PMCSellDetailViewModel>.Create()
            )
        { }

        public PMCEvcProxy(IAnatoliProxy<PMCEvcDetailViewModel, PMCSellDetailViewModel> evcDetailProxy
            )
        {
            PMCEvcDetailProxy = evcDetailProxy;
        }
        #endregion

        public override PMCSellViewModel Convert(PMCEvcViewModel data)
        {
            return new PMCSellViewModel()
            {
                SellDetail = PMCEvcDetailProxy.Convert(data.EVCDetail),
            };
        }

        public override PMCEvcViewModel ReverseConvert(PMCSellViewModel data)
        {
            return new PMCEvcViewModel()
            {
                EVCDetail = PMCEvcDetailProxy.ReverseConvert(data.SellDetail),
            };
        }
    }
}
