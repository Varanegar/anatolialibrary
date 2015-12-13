using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.ViewModels.Product;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.Business.Proxy.Concretes
{
    public class ProductGroupProxy : AnatoliProxy<ProductGroup, ProductGroupViewModel>, IAnatoliProxy<ProductGroup, ProductGroupViewModel>
    {
        public override ProductGroupViewModel Convert(ProductGroup data)
        {
            return new ProductGroupViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateLabelKey = data.PrivateLabelOwner.Id,
                ParentId = data.ProductGroup2.Number_ID,
                ParentUniqueId = data.ProductGroup2.Id,
                NRight = data.NRight,
                NLevel = data.NLevel,
                NLeft = data.NLeft,
                GroupName = data.GroupName,
                //CharGroupId
            };
        }

        public override ProductGroup ReverseConvert(ProductGroupViewModel data)
        {
            return new ProductGroup
            {
                Number_ID = data.ID,
                Id = data.UniqueId,

                NRight = data.NRight,
                NLevel = data.NLevel,
                NLeft = data.NLeft,
                GroupName = data.GroupName,

                PrivateLabelOwner = new Principal { Id = data.PrivateLabelKey },
                ProductGroup2 = new ProductGroup { Id = data.ParentUniqueId, Number_ID = data.ID },
            };
        }
    }
}