using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.Concretes.ProductConcretes
{
    public class BrandProxy : AnatoliProxy<Brand, BrandViewModel>, IAnatoliProxy<Brand, BrandViewModel>
    {
        public override BrandViewModel Convert(Brand data)
        {
            return new BrandViewModel
            {
                ID = data.Number_ID,
                IsRemoved = data.IsRemoved,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,
                BrandName = data.BrandName
            };
        }

        public override Brand ReverseConvert(BrandViewModel data)
        {
            return new Brand
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                IsRemoved = data.IsRemoved,
                BrandName = data.BrandName,

                ApplicationOwnerId = data.ApplicationOwnerId,
            };
        }

    }
}