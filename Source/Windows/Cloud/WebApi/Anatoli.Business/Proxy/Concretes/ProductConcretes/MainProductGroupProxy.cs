using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class MainProductGroupProxy : AnatoliProxy<MainProductGroup, MainProductGroupViewModel>, IAnatoliProxy<MainProductGroup, MainProductGroupViewModel>
    {
        public override MainProductGroupViewModel Convert(MainProductGroup data)
        {
            return new MainProductGroupViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,
                ParentId = data.ProductGroup2 != null ? data.ProductGroup2.Number_ID : -1,
                ParentUniqueIdString = data.ProductGroup2 != null ? data.ProductGroup2.Id.ToString() : Guid.Empty.ToString(),
                NRight = data.NRight,
                NLevel = data.NLevel,
                NLeft = data.NLeft,
                GroupName = data.GroupName,
                IsRemoved = data.IsRemoved,

                //CharGroupId
            };
        }

        public override MainProductGroup ReverseConvert(MainProductGroupViewModel data)
        {
            MainProductGroup pg = new MainProductGroup()
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