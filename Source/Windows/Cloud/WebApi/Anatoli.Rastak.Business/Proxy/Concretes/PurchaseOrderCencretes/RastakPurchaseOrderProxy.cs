using Anatoli.Rastak.Business.Proxy.Interfaces;
using Anatoli.Rastak.DataAccess.DataAdapter;
using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.DataAccess.Helpers.Entity;
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
    public class RastakPurchaseOrderProxy : AnatoliProxy<RastakSellViewModel, PurchaseOrderViewModel>, IAnatoliProxy<RastakSellViewModel, PurchaseOrderViewModel>
    {       
        public IAnatoliProxy<RastakSellDetailViewModel, PurchaseOrderLineItemViewModel> RastakSellDetailProxy { get; set; }
        #region Ctors
        public RastakPurchaseOrderProxy() :
            this(AnatoliProxy<RastakSellDetailViewModel, PurchaseOrderLineItemViewModel>.Create()
            )
        { }

        public RastakPurchaseOrderProxy(IAnatoliProxy<RastakSellDetailViewModel, PurchaseOrderLineItemViewModel> sellDetailProxy
            )
        {
            RastakSellDetailProxy = sellDetailProxy;
        }
        #endregion

        public override PurchaseOrderViewModel Convert(RastakSellViewModel data, RastakStoreConfigEntity storeConfig)
        {
            return new PurchaseOrderViewModel()
            {
                Amount = data.Amount,
                AppOrderNo = data.RequestNo,
                ChargeAmount = data.ChargeAmount,
                Comment = data.Comment,
                DiscountAmount = data.DiscountAmount,
                DiscountAmount2 = data.DiscountAmount2,
                NetAmount = data.NetAmount,
                OrderPDate = data.RequestDateTime,
                PayableAmount = data.PayableAmount,
                TaxAmount = data.TaxAmount,

                LineItems = RastakSellDetailProxy.Convert(data.SellDetail, storeConfig),
            };
        }

        public override RastakSellViewModel ReverseConvert(PurchaseOrderViewModel data, RastakStoreConfigEntity storeConfig)
        {
            //int currentCustomerId = CustomerAdapter.Instance.GetCustomerId(data.UserId);
            int fiscalYearId = GeneralCommands.GetFiscalYearId(null);
            return new RastakSellViewModel()
            {
                AppUserId = storeConfig.AppUserId,
                CenterId = storeConfig.CenterId,
                Amount = data.Amount,
                ChargeAmount = data.ChargeAmount,
                Comment = data.Comment,
                DiscountAmount = data.DiscountAmount,
                DiscountAmount2 = data.DiscountAmount2,
                FiscalYearId = fiscalYearId,
                Freight = data.FreightAmount,
                FreightCustomerAmount = data.FreightAmount,
                InvoiceDate = data.OrderPDate,
                InvoiceDateTime = data.OrderPDate,
                ManualDiscount = 0,
                ModifiedDate = DateTime.Now,
                NetAmount = data.NetAmount,
                PayableAmount = data.PayableAmount,
                RequestNo = data.AppOrderNo,
                RequestDateTime = data.OrderPDate,
                SalesmanId = storeConfig.SalesmanId,
                SellCategoryId = 2, //فاکتور غير حضوري
                SellStatusId = 1, //پيش نويس
                SellTypeId = 1, //1	طبق جدول تخفيفات
                SLId = storeConfig.SLId,
                StockId = storeConfig.StockId,
                TaxAmount = data.TaxAmount,
                IsConfirmed = false,
                PayTypeId = 1, //نقد 


                SellDetail = RastakSellDetailProxy.ReverseConvert(data.LineItems, storeConfig),
            };
        }
    }
}
