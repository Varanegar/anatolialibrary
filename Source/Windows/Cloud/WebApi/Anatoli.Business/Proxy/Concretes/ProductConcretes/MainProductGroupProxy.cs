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
                PrivateOwnerId = data.PrivateLabelOwner.Id,
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

                NRight = data.NRight,
                NLevel = data.NLevel,
                NLeft = data.NLeft,
                GroupName = data.GroupName,
                IsRemoved = data.IsRemoved,

                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },
            };

            if (data.ParentUniqueIdString != null && data.ParentUniqueIdString != "")
                pg.ProductGroup2Id = Guid.Parse(data.ParentUniqueIdString);

            if (data.UniqueIdString != null && data.UniqueIdString != "")
                pg.Id = Guid.Parse(data.UniqueIdString);
            
            return pg;
        }
    }
}