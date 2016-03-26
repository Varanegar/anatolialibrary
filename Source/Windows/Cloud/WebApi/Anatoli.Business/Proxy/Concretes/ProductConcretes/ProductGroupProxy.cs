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
                ApplicationOwnerId = data.ApplicationOwnerId,
                ParentId = -1,
                ParentUniqueIdString = data.ProductGroup2Id != null ? data.ProductGroup2Id.ToString() : Guid.Empty.ToString(),
                NRight = data.NRight,
                NLevel = data.NLevel,
                NLeft = data.NLeft,
                GroupName = data.GroupName,
                IsRemoved = data.IsRemoved,
            };
        }

        public override ProductGroup ReverseConvert(ProductGroupViewModel data)
        {
            ProductGroup pg = new ProductGroup()
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ProductGroup2Id = data.ParentUniqueId,

                NRight = data.NRight,
                NLevel = data.NLevel,
                NLeft = data.NLeft,
                GroupName = data.GroupName,
                IsRemoved = data.IsRemoved,

                ApplicationOwnerId = data.ApplicationOwnerId,
            };

            if (pg.ProductGroup2Id == Guid.Empty)
                pg.ProductGroup2Id = null;

            return pg;
        }
    }
}