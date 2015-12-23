using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Proxy.Concretes.ProductConcretes
{
    public class BasketItemProxy : AnatoliProxy<BasketItem, BasketItemViewModel>, IAnatoliProxy<BasketItem, BasketItemViewModel>
    {
        public IAnatoliProxy<Product, ProductViewModel> ProductProxy { get; set; }

        public override BasketItemViewModel Convert(BasketItem data)
        {
            return new BasketItemViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateLabelKey = data.PrivateLabelOwner.Id,
                Comment = data.Comment,
                Qty = data.Qty,
                Product = (data.Product == null)? null: ProductProxy.Convert(data.Product),
            };
        }

        public override BasketItem ReverseConvert(BasketItemViewModel data)
        {
            return new BasketItem
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                Comment = data.Comment,
                Qty = data.Qty,
                Product = (data.Product == null) ? null : ProductProxy.ReverseConvert(data.Product),

                PrivateLabelOwner = new Principal { Id = data.PrivateLabelKey },
            };
        }
    }
}