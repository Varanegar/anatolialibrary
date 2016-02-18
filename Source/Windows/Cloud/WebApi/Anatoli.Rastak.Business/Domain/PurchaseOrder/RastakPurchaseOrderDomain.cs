using Anatoli.Rastak.Business.Proxy;
using Anatoli.Rastak.Business.Proxy.Interfaces;
using Anatoli.Rastak.DataAccess.DataAdapter;
using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.ViewModels.Base;
using Anatoli.Rastak.ViewModels.EVC;
using Anatoli.Rastak.ViewModels.Order;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Order;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Rastak.Business.Domain.PurchaseOrder
{
    public class RastakPurchaseOrderDomain : RastakBusinessDomain<RastakSellViewModel, PurchaseOrderViewModel>, IRastakBusinessDomain<RastakSellViewModel, PurchaseOrderViewModel>
    {
        protected IAnatoliProxy<RastakEvcViewModel, RastakSellViewModel> EvcProxy { get; set; }
        protected IAnatoliProxy<RastakCustomerViewModel, CustomerViewModel> CustomerProxy { get; set; }

        #region Ctors
        public RastakPurchaseOrderDomain()
            : this(AnatoliProxy<RastakSellViewModel, PurchaseOrderViewModel>.Create(), AnatoliProxy<RastakEvcViewModel, RastakSellViewModel>.Create(),
             AnatoliProxy<RastakCustomerViewModel, CustomerViewModel>.Create())
        { }
        public RastakPurchaseOrderDomain(IAnatoliProxy<RastakSellViewModel, PurchaseOrderViewModel> proxy, IAnatoliProxy<RastakEvcViewModel, RastakSellViewModel> evcProxy,
             IAnatoliProxy<RastakCustomerViewModel, CustomerViewModel> customerProxy)
        {
            Proxy = proxy;
            EvcProxy = evcProxy;
            CustomerProxy = customerProxy;
        }
        #endregion

        #region Methods
        public List<PurchaseOrderViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
        public List<PurchaseOrderViewModel> GetAllByCustomerId(string customerId, string statusId, string centerId)
        {
            var orders = SellAdapter.Instance.GetPurchaseOrderByCustomerId(customerId, statusId, centerId);
            return orders;
        }

        public List<PurchaseOrderViewModel> GetAllChangedAfter(DateTime selectedDate)
        {
            throw new NotImplementedException();
        }

        public PurchaseOrderViewModel Publish(PurchaseOrderViewModel baseViewModels)
        {
            baseViewModels = GetPerformaInvoicePreview(baseViewModels);
            var storeConfig = StoreConfigHeler.Instance.GetStoreConfig(baseViewModels.StoreGuid.ToString());
            var data = Proxy.ReverseConvert(baseViewModels, storeConfig);

            var customerData = CustomerProxy.ReverseConvert(baseViewModels.Customer, storeConfig);
            baseViewModels.AppOrderNo = SellAdapter.Instance.SavePurchaseOrder(data, customerData);
            return baseViewModels;
        }

        public PurchaseOrderViewModel GetPerformaInvoicePreview(PurchaseOrderViewModel baseViewModels)
        {
            var storeConfig = StoreConfigHeler.Instance.GetStoreConfig(baseViewModels.StoreGuid.ToString());
            var RastakSell = Proxy.ReverseConvert(baseViewModels, storeConfig);

            RastakSell.CustomerId = null;

            var resultEvc = EVCAdapter.Instance.CalcEvcResult(EvcProxy.ReverseConvert(RastakSell, storeConfig));
            RastakSell = SetSellDataByEvc(RastakSell, resultEvc);
            var resultPurchaseOrder = Proxy.Convert(RastakSell, storeConfig);
            resultPurchaseOrder = SetHeaderData(resultPurchaseOrder, baseViewModels);
            return resultPurchaseOrder;
        }

        private PurchaseOrderViewModel SetHeaderData(PurchaseOrderViewModel result, PurchaseOrderViewModel source)
        {
            result.StoreGuid = source.StoreGuid;
            result.Customer = source.Customer;
            result.UserId = source.UserId;
            result.AppOrderNo = source.AppOrderNo;
            result.Comment = source.Comment;
            result.UniqueId = source.UniqueId;
            return result;
        }
        private RastakSellViewModel SetSellDataByEvc(RastakSellViewModel sellData, RastakEvcViewModel evcData)
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

        private RastakSellViewModel SetSellDetailDataByEvcDetail(RastakSellViewModel sellData, RastakEvcDetailViewModel evcDetailData)
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
                RastakSellDetailViewModel data = new RastakSellDetailViewModel();
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
