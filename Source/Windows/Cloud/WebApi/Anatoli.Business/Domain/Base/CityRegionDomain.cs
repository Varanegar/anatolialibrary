﻿using System;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.BaseModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class CityRegionDomain : BusinessDomainV2<CityRegion, CityRegionViewModel, CityRegionRepository, ICityRegionRepository>, IBusinessDomainV2<CityRegion, CityRegionViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public CityRegionDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public CityRegionDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(CityRegion currentCityRegion, CityRegion item)
        {
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
                    MainRepository.Update(currentCityRegion);
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
