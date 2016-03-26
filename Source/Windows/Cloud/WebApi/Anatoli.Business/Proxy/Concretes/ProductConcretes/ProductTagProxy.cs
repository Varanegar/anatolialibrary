using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class ProductTagProxy : AnatoliProxy<ProductTag, ProductTagViewModel>, IAnatoliProxy<ProductTag, ProductTagViewModel>
    {
        public override ProductTagViewModel Convert(ProductTag data)
        {
            return new ProductTagViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                IsRemoved = data.IsRemoved,
                ApplicationOwnerId = data.ApplicationOwnerId,
                ProductTagName = data.ProductTagName,
            };
        }

        public override ProductTag ReverseConvert(ProductTagViewModel data)
        {
            return new ProductTag
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                IsRemoved = data.IsRemoved,
                ProductTagName = data.ProductTagName,

                ApplicationOwnerId = data.ApplicationOwnerId ,
            };
        }
    }
}