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
using Anatoli.ViewModels.CustomerModels;
using System.Data.Entity;
using Anatoli.ViewModels.VnGisModels;
using AutoMapper.QueryableExtensions;

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
         public async Task<List<RegionAreaViewModel>> GetAreaLevel1()
         {
             return await GetAllAsync(p => p.RegionAreaParentId == null);
         }

         public async Task<List<RegionAreaViewModel>> GetAreaPathById(Guid regionAreaId)
         {
             var list = new List<RegionAreaViewModel>();
             if (regionAreaId != null)
             {
                 var entity = await GetByIdAsync(regionAreaId);
                 while (entity.ParentId != null)
                 {
                     list.Add(entity);
                     entity = await GetByIdAsync((Guid)entity.ParentId);

                 }
                 list.Add(entity);
             }

             return list;
         }
         public async Task<List<CustomerViewModel>> GetRegionAreaSelectedCustomers(Guid regionAreaId, bool getSelected)
         {
             RegionArea regionArea = await MainRepository.GetQuery()
                    .Where(p => p.ApplicationOwnerId == ApplicationOwnerKey && p.DataOwnerId == DataOwnerKey && p.Id == regionAreaId)
                    .AsNoTracking().FirstAsync();
             
             var result = await new CustomerDomain(ApplicationOwnerKey, DataOwnerKey, DataOwnerCenterKey).GetCustomersByLocation(regionArea.AreaLocation, true);

             return null;
         }
        #endregion
    }
}
