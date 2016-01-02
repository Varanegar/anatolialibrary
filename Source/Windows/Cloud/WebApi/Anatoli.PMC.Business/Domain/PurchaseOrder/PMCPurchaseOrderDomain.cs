using Anatoli.PMC.Business.Proxy;
using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.DataAdapter;
using Anatoli.PMC.DataAccess.Helpers;
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
            var storeConfig = StoreConfigHeler.Instance.GetStoreConfig(baseViewModels.StoreGuid.ToString());
            var data = Proxy.ReverseConvert(baseViewModels, storeConfig);
            SellAdapter.Instance.SavePurchaseOrder(data);
        }

        public PurchaseOrderViewModel GetPerformaInvoicePreview(PurchaseOrderViewModel baseViewModels)
        {
            var storeConfig = StoreConfigHeler.Instance.GetStoreConfig(baseViewModels.StoreGuid.ToString());
            var pmcSell = Proxy.ReverseConvert(baseViewModels, storeConfig);
            var resultEvc = EVCAdapter.Instance.CalcEvcResult(EvcProxy.ReverseConvert(pmcSell, storeConfig));
            pmcSell = SetSellDataByEvc(pmcSell, resultEvc);
            var resultPurchaseOrder = Proxy.Convert(pmcSell, storeConfig);
            return resultPurchaseOrder;
        }

        private PMCSellViewModel SetSellDataByEvc(PMCSellViewModel sellData, PMCEvcViewModel evcData)
        {
            sellData.Amount = evcData.Amount;
            sellData.ChargeAmount = evcData.ChargeAmount;
            sellData.DiscountAmount = evcData.DiscountAmount;
            sellData.DiscountAmount2 = evcData.DiscountAmount2;
            sellData.TaxAmount = evcData.TaxAmount;
            sellData.NetAmount = evcData.NetAmount;

            evcData.EVCDetail.ForEach(item =>
                {
                    sellData = SetSellDetailDataByEvcDetail(sellData, item);
                });

            return sellData;
        }

        private PMCSellViewModel SetSellDetailDataByEvcDetail(PMCSellViewModel sellData, PMCEvcDetailViewModel evcDetailData)
        {
            bool isProductExists = false;
            sellData.SellDetail.ForEach(item =>
                {
                    if(item.ProductId == evcDetailData.ProductId && item.IsPrize == evcDetailData.IsPrize)
                    {
                        if(evcDetailData.IsPrize)
                        {
                            isProductExists = true;
                            item.Qty += evcDetailData.Qty;
                        }
                        else
                        {
                            item.Qty = evcDetailData.Qty;
                            item.UnitPrice = evcDetailData.UnitPrice;
                            item.TaxAmount = evcDetailData.TaxAmount;
                            item.ChargeAmount = evcDetailData.ChargeAmount;
                            item.Amount = evcDetailData.AmountCalcBase;
                            item.NetAmount = evcDetailData.NetAmount;
                            item.DiscountAmount = evcDetailData.DiscountAmount;
                        }
                    }
                });
            if(!isProductExists && evcDetailData.IsPrize)
            {
                PMCSellDetailViewModel data = new PMCSellDetailViewModel();
                data.IsPrize = true;
                data.SellId = sellData.SellId;
                data.ProductId = evcDetailData.ProductId;
                data.Qty = evcDetailData.Qty;
                sellData.SellDetail.Add(data);
            }
            return sellData;
        }
        #endregion
    }
}
