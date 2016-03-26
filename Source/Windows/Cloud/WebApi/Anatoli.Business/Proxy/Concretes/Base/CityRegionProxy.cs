using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class CityRegionProxy : AnatoliProxy<CityRegion, CityRegionViewModel>, IAnatoliProxy<CityRegion, CityRegionViewModel>
    {
        public override CityRegionViewModel Convert(CityRegion data)
        {
            return new CityRegionViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,
                NLeft = data.NLeft,
                NRight = data.NRight,
                NLevel = data.NLevel,
                GroupName = data.GroupName,
                IsRemoved = data.IsRemoved,
                //ParentId = data.CityRegion2 != null ? data.CityRegion2.Number_ID : -1,
                ParentUniqueIdString = data.CityRegion2Id.ToString(),
                Priority = data.Priority,
            };
        }

        public override CityRegion ReverseConvert(CityRegionViewModel data)
        {
            CityRegion tempRegion = new CityRegion()
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                IsRemoved = data.IsRemoved,

                NLeft = data.NLeft,
                NRight = data.NRight,
                NLevel = data.NLevel,
                GroupName = data.GroupName,
                Priority = data.Priority,

                ApplicationOwnerId = data.ApplicationOwnerId,

            };

            if (data.ParentUniqueIdString != null && data.ParentUniqueIdString != "")
                tempRegion.CityRegion2Id = Guid.Parse(data.ParentUniqueIdString);

            return tempRegion;
        }
    }
}