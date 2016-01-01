using Anatoli.PMC.Business.Proxy;
using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.DataAdapter;
using Anatoli.PMC.ViewModels.EVC;
using Anatoli.PMC.ViewModels.Order;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Domain.PurchaseOrder
{
    public class PMCPurchaseOrderDomain : PMCBusinessDomain<PMCSellViewModel, PurchaseOrderViewModel>, IPMCBusinessDomain<PMCSellViewModel, PurchaseOrderViewModel>
    {
        protected IAnatoliProxy<PMCEvcViewModel, PMCSellViewModel> EvcProxy { get; set; }

        #region Ctors
        public PMCPurchaseOrderDomain()
            : this(AnatoliProxy<PMCSellViewModel, PurchaseOrderViewModel>.Create(), AnatoliProxy<PMCEvcViewModel, PMCSellViewModel>.Create())
        { }
        public PMCPurchaseOrderDomain(IAnatoliProxy<PMCSellViewModel, PurchaseOrderViewModel> proxy, IAnatoliProxy<PMCEvcViewModel, PMCSellViewModel> evcProxy)
        {
            Proxy = proxy;
            EvcProxy = evcProxy;
        }
        #endregion

        #region Methods
        public List<PurchaseOrderViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<PurchaseOrderViewModel> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public void Publish(PurchaseOrderViewModel baseViewModels)
        {
            var data = Proxy.ReverseConvert(baseViewModels);
            SellAdapter.SavePurchaseOrder(data);
        }

        public PurchaseOrderViewModel GetPerformaInvoicePreview(PurchaseOrderViewModel baseViewModels)
        {
            var data = Proxy.ReverseConvert(baseViewModels);
            var evcData = EvcProxy.ReverseConvert(data);
            var resultEvc = EVCAdapter.CalcEvcResult(evcData);
            var resultPurchaseOrder = Proxy.Convert(EvcProxy.Convert(resultEvc));
            return resultPurchaseOrder;
        }
        #endregion
    }
}
