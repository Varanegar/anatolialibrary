using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class ProductTagValueProxy : AnatoliProxy<ProductTagValue, ProductTagValueViewModel>, IAnatoliProxy<ProductTagValue, ProductTagValueViewModel>
    {
        public override ProductTagValueViewModel Convert(ProductTagValue data)
        {
            return new ProductTagValueViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner_Id,
                IsRemoved = data.IsRemoved,

                ProductId = data.ProductId,
                ProductTagId = data.ProductTagId,
                FromDate = data.FromDate,
                FromPDate = data.FromPDate,
                ToDate = data.ToDate,
                ToPDate = data.ToPDate,
            };
        }

        public override ProductTagValue ReverseConvert(ProductTagValueViewModel data)
        {
            return new ProductTagValue
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                IsRemoved = data.IsRemoved,

                ProductId = data.ProductId,
                ProductTagId = data.ProductTagId,
                FromDate = data.FromDate,
                FromPDate = data.FromPDate,
                ToDate = data.ToDate,
                ToPDate = data.ToPDate,
                PrivateLabelOwner_Id = data.PrivateOwnerId ,
            };
        }
    }
}