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
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Domain
{
    public class CityRegionDomain : BusinessDomain<CityRegionViewModel>, IBusinessDomain<CityRegion, CityRegionViewModel>
    {
        #region Properties
        public IAnatoliProxy<CityRegion, CityRegionViewModel> Proxy { get; set; }
        public IRepository<CityRegion> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        CityRegionDomain() { }
        public CityRegionDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public CityRegionDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new CityRegionRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<CityRegion, CityRegionViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public CityRegionDomain(ICityRegionRepository cityRegionRepository, IPrincipalRepository principalRepository, IAnatoliProxy<CityRegion, CityRegionViewModel> proxy)
        {
            Proxy = proxy;
            Repository = cityRegionRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<CityRegionViewModel>> GetAll()
        {
            var cityRegions = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(cityRegions.ToList()); ;
        }

        public async Task<List<CityRegionViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var cityRegions = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(cityRegions.ToList()); ;
        }

        public async Task<List<CityRegionViewModel>> PublishAsync(List<CityRegionViewModel> dataViewModels)
        {
            try
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var cityRegions = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                foreach (CityRegion item in cityRegions)
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentCityRegion = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (currentCityRegion != null)
                    {
                        if (currentCityRegion.GroupName != item.GroupName ||
                            currentCityRegion.NLeft != item.NLeft ||
                            currentCityRegion.NRight != item.NRight ||
                            currentCityRegion.NLevel != item.NLevel ||
                            currentCityRegion.CityRegion2Id != item.CityRegion2Id)
                        {
                            currentCityRegion.LastUpdate = DateTime.Now;
                            currentCityRegion.GroupName = item.GroupName;
                            currentCityRegion.NLeft = item.NLeft;
                            currentCityRegion.NRight = item.NRight;
                            currentCityRegion.NLevel = item.NLevel;
                            currentCityRegion.CityRegion2Id = item.CityRegion2Id;
                            await Repository.UpdateAsync(currentCityRegion);
                        }
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        await Repository.AddAsync(item);
                    }
                };
                await Repository.SaveChangesAsync();
                
            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            finally
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                log.Info("PublishAsync Finish" + dataViewModels.Count);
            }
            return dataViewModels;

        }
        public async Task<List<CityRegionViewModel>> CheckDeletedAsync(List<CityRegionViewModel> dataViewModels)
        {
            try
            {
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentDataList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                currentDataList.ForEach(item =>
                {
                    if (dataViewModels.Find(p => p.UniqueId == item.Id) == null)
                    {
                        item.LastUpdate = DateTime.Now;
                        item.IsRemoved = true;
                        Repository.UpdateAsync(item);
                    }
                });

                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("CheckForDeletedAsync", ex);
                throw ex;
            }

            return dataViewModels;
        }

        public async Task<List<CityRegionViewModel>> Delete(List<CityRegionViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var cityRegions = Proxy.ReverseConvert(dataViewModels);

                cityRegions.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }


        #endregion
    }
}
