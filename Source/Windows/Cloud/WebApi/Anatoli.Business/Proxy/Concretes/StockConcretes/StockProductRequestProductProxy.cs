using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockProductRequestProductConcretes
{
    public class StockProductRequestProductProxy : AnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel>, IAnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel>
    {
        public IAnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel> StockProductRequestProductDetailProxy { get; set; }
        #region Ctors
        public StockProductRequestProductProxy() :
            this(AnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel>.Create()
            )
        { }

        public StockProductRequestProductProxy(IAnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel> stockProductRequestProductDetailProxy
            )
        {
            StockProductRequestProductDetailProxy = stockProductRequestProductDetailProxy;
        }
        #endregion
        public override StockProductRequestProductViewModel Convert(StockProductRequestProduct data)
        {
            return new StockProductRequestProductViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,

                Accepted1Qty = data.Accepted1Qty,
                Accepted2Qty = data.Accepted2Qty,
                Accepted3Qty = data.Accepted3Qty,
                DeliveredQty = data.DeliveredQty,
                ProductId = data.ProductId,
                RequestQty = data.RequestQty,
                StockProductRequestId = data.StockProductRequestId,

                StockProductRequestProductDetails = (data.StockProductRequestProductDetails == null) ? null : StockProductRequestProductDetailProxy.Convert(data.StockProductRequestProductDetails.ToList()),
            };
        }

        public override StockProductRequestProduct ReverseConvert(StockProductRequestProductViewModel data)
        {
            return new StockProductRequestProduct
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

                Accepted1Qty = data.Accepted1Qty,
                Accepted2Qty = data.Accepted2Qty,
                Accepted3Qty = data.Accepted3Qty,
                DeliveredQty = data.DeliveredQty,
                ProductId = data.ProductId,
                RequestQty = data.RequestQty,
                StockProductRequestId = data.StockProductRequestId,

                StockProductRequestProductDetails = (data.StockProductRequestProductDetails == null) ? null : StockProductRequestProductDetailProxy.ReverseConvert(data.StockProductRequestProductDetails),
            };
        }
    }
}