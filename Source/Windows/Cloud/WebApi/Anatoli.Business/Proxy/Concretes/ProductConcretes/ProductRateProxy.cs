using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.Concretes.ProductConcretes
{
    public class ProductRateProxy : AnatoliProxy<ProductRate, ProductRateViewModel>, IAnatoliProxy<ProductRate, ProductRateViewModel>
    {
        public override ProductRateViewModel Convert(ProductRate data)
        {
            return new ProductRateViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,
                ProductGuid = data.ProductId,
                RateBy = data.RateBy,
                ProductRateValue = data.Value,
                RateByName = data.RateByName,
                RateDate = data.RateDate,
                RateTime = data.RateTime,

            };
        }

        public override ProductRate ReverseConvert(ProductRateViewModel data)
        {
            return new ProductRate
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ProductId = data.ProductGuid,
                RateBy = data.RateBy,
                RateByName = data.RateByName,
                RateDate = data.RateDate,
                RateTime = data.RateTime,
                Value = data.ProductRateValue,
                ApplicationOwnerId = data.ApplicationOwnerId,
            };
        }
    }
}