using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;
using Anatoli.DataAccess.Models.Route;
using Anatoli.ViewModels.Route;

namespace Anatoli.Business.Domain.Route
{
    public class RegionAreaDomain : BusinessDomainV2<RegionArea, RegionAreaViewModel, RegionAreaRepository, IRegionAreaRepository>, IBusinessDomainV2<RegionArea, RegionAreaViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
         public RegionAreaDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
         public RegionAreaDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
         protected override void AddDataToRepository(RegionArea currentData, RegionArea item)
         {
             if (currentData != null)
             {
                 if (currentData.AreaName != item.AreaName)
                 {
                     currentData.AreaName = item.AreaName;
                     currentData.LastUpdate = DateTime.Now;
                     MainRepository.Update(currentData);
                 }
             }
             else
             {
                 item.CreatedDate = item.LastUpdate = DateTime.Now;
                 MainRepository.Add(item);
             }
         }

        #endregion
    }
}
