using System;
using System.Linq;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models.Route;
using Anatoli.ViewModels.VnGisModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain.Route
{
    public class RegionAreaPointDomain : BusinessDomainV2<RegionAreaPoint, RegionAreaPointViewModel, RegionAreaPointRepository, IRegionAreaPointRepository>, IBusinessDomainV2<RegionAreaPoint, RegionAreaPointViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
         public RegionAreaPointDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
         public RegionAreaPointDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
         protected override void AddDataToRepository(RegionAreaPoint currentData, RegionAreaPoint item)
         {
             if (currentData != null)
             {
                 currentData.Latitude = item.Latitude;
                 currentData.Longitude = item.Longitude;
                 currentData.Priority = item.Priority;
                 currentData.RegionAreaId = item.RegionAreaId;
                 currentData.LastUpdate = DateTime.Now;
                MainRepository.Update(currentData);
             }
             else
             {
                 item.CreatedDate = item.LastUpdate = DateTime.Now;
                 MainRepository.Add(item);
             }
         }

         public bool HasRegionAreaPoint(Guid regionAreaId)
         {
             return (((AnatoliDbContext)DBContext).RegionAreaPoints.Where(x => x.RegionAreaId == regionAreaId).Count() > 3);
         }
        #endregion
    }
}
