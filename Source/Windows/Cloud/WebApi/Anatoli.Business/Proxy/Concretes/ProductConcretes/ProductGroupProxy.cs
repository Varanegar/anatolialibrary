using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
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
                ParentId = data.ProductGroup2 != null ? data.ProductGroup2.Number_ID : -1,
                ParentUniqueIdString = data.ProductGroup2 != null ? data.ProductGroup2.Id.ToString() : Guid.Empty.ToString(),
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
                ProductGroup2 = new ProductGroup { Id = Guid.Parse(data.ParentUniqueIdString), Number_ID = data.ParentId },
            };
        }
    }
}