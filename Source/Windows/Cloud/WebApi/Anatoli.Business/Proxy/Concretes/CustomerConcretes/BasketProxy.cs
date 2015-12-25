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
    public class BasketProxy : AnatoliProxy<Basket, BasketViewModel>, IAnatoliProxy<Basket, BasketViewModel>
    {
        public IAnatoliProxy<BasketItem, BasketItemViewModel> BasketItemProxy { get; set; }

        #region Ctors
        public BasketProxy() :
            this(AnatoliProxy<BasketItem, BasketItemViewModel>.Create()
            )
        { }

        public BasketProxy(IAnatoliProxy<BasketItem, BasketItemViewModel> basketItemProxy
            )
        {
            BasketItemProxy = basketItemProxy;
        }
        #endregion

        public override BasketViewModel Convert(Basket data)
        {
            return new BasketViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateLabelKey = data.PrivateLabelOwner.Id,
                BasketName = data.BasketName,
                BasketTypeValueId = data.BasketTypeValueGuid,

                BasketItems = (data.BasketItems == null) ? null : BasketItemProxy.Convert(data.BasketItems.ToList()),
            };
        }

        public override Basket ReverseConvert(BasketViewModel data)
        {
            return new Basket
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                BasketName = data.BasketName,
                BasketTypeValueGuid = data.BasketTypeValueId,

                PrivateLabelOwner = new Principal { Id = data.PrivateLabelKey },

                BasketItems = (data.BasketItems == null) ? null : BasketItemProxy.ReverseConvert(data.BasketItems),
            };
        }
    }
}