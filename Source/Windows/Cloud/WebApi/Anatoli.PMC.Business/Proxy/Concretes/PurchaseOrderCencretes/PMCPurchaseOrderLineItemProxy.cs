using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.DataAdapter;
using Anatoli.PMC.DataAccess.Helpers.Entity;
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
    public class PMCPurchaseOrderLineItem : AnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel>, IAnatoliProxy<PMCSellDetailViewModel, PurchaseOrderLineItemViewModel>
    {
        public override PurchaseOrderLineItemViewModel Convert(PMCSellDetailViewModel data, PMCStoreConfigEntity storeConfig)
        {
            Guid currentProductUniqueId = Guid.Parse(ProductAdapter.Instance.GetProductUniqueId(data.ProductId));
            return new PurchaseOrderLineItemViewModel()
            {
                ChargeAmount = data.ChargeAmount,
                Comment = "",
                DiscountAmount = data.DiscountAmount,
                IsPrize = data.IsPrize,
                NetAmount = data.NetAmount,
                PriceId = data.PriceId,
                ProductId = currentProductUniqueId,
                Qty = System.Convert.ToDecimal(data.Qty),
                TaxAmount = data.TaxAmount,
                UnitPrice = data.UnitPrice,

            };
        }

        public override PMCSellDetailViewModel ReverseConvert(PurchaseOrderLineItemViewModel data, PMCStoreConfigEntity storeConfig)
        {
            int currentProductId = ProductAdapter.Instance.GetProductId(data.ProductId);
            return new PMCSellDetailViewModel()
            {
                AddPercent = null,
                AppUserId = storeConfig.AppUserId,
                ModifiedDate = DateTime.Now,
                DiscountPercent = null,
                IsPrize = false,
                PriceId = data.PriceId,
                NetAmount = data.NetAmount,
                ChargeAmount = data.ChargeAmount,
                TaxAmount = data.TaxAmount,
                UnitPrice = data.UnitPrice,
                DiscountAmount = data.DiscountAmount,
                PackQty = 0,
                ProductId = currentProductId,
                Qty = data.Qty,
                RequestQty = data.Qty,
            };
        }
    }
}
