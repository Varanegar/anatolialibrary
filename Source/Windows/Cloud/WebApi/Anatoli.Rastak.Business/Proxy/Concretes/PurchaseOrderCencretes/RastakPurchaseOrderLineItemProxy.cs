using Anatoli.Rastak.Business.Proxy.Interfaces;
using Anatoli.Rastak.DataAccess.DataAdapter;
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
    public class RastakPurchaseOrderLineItem : AnatoliProxy<RastakSellDetailViewModel, PurchaseOrderLineItemViewModel>, IAnatoliProxy<RastakSellDetailViewModel, PurchaseOrderLineItemViewModel>
    {
        public override PurchaseOrderLineItemViewModel Convert(RastakSellDetailViewModel data, RastakStoreConfigEntity storeConfig)
        {
            Guid currentProductUniqueId = Guid.Parse(ProductAdapter.Instance.GetProductUniqueId(data.ProductId));
            return new PurchaseOrderLineItemViewModel()
            {
                ChargeAmount = data.ChargeAmount,
                Comment = "",
                DiscountAmount = data.DiscountAmount,
                IsPrize = data.IsPrize,
                NetAmount = data.NetAmount,
                ProductId = currentProductUniqueId,
                Qty = System.Convert.ToDecimal(data.Qty),
                TaxAmount = data.TaxAmount,
                UnitPrice = data.UnitPrice,

            };
        }

        public override RastakSellDetailViewModel ReverseConvert(PurchaseOrderLineItemViewModel data, RastakStoreConfigEntity storeConfig)
        {
            int currentProductId = ProductAdapter.Instance.GetProductId(data.ProductId);
            return new RastakSellDetailViewModel()
            {
                AddPercent = null,
                AppUserId = storeConfig.AppUserId,
                ModifiedDate = DateTime.Now,
                DiscountPercent = null,
                IsPrize = false,
                PackQty = 0,
                ProductId = currentProductId,
                Qty = System.Convert.ToDouble(data.Qty),
                RequestQty = System.Convert.ToDouble(data.Qty),
            };
        }
    }
}
