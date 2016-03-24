using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using Anatoli.ViewModels.StockModels;
using Anatoli.Business.Proxy.Interfaces;

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
                ApplicationOwnerId = data.ApplicationOwnerId,

                Accepted1Qty = data.Accepted1Qty,
                Accepted2Qty = data.Accepted2Qty,
                Accepted3Qty = data.Accepted3Qty,
                DeliveredQty = data.DeliveredQty,
                ProductId = data.ProductId,
                RequestQty = data.RequestQty,
                StockProductRequestId = data.StockProductRequestId,

                StockProductRequestProductDetails = (data.StockProductRequestProductDetails == null) ? null : StockProductRequestProductDetailProxy.Convert(data.StockProductRequestProductDetails.ToList()),

                ProductCode = data.Product.ProductCode,
                ProductName = data.Product.ProductName,

                StockLevelQty = data.StockLevelQty,
            };
        }

        public override StockProductRequestProduct ReverseConvert(StockProductRequestProductViewModel data)
        {
            return new StockProductRequestProduct
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ApplicationOwnerId = data.ApplicationOwnerId,
                CreatedDate = DateTime.Now,
                LastUpdate = DateTime.Now,

                Accepted1Qty = data.Accepted1Qty,
                Accepted2Qty = data.Accepted2Qty,
                Accepted3Qty = data.Accepted3Qty,
                DeliveredQty = data.DeliveredQty,
                ProductId = data.ProductId,
                RequestQty = data.RequestQty,
                StockProductRequestId = data.StockProductRequestId,

                StockProductRequestProductDetails = (data.StockProductRequestProductDetails == null) ? null : StockProductRequestProductDetailProxy.ReverseConvert(data.StockProductRequestProductDetails),

                StockLevelQty = data.StockLevelQty,
            };
        }
    }
}