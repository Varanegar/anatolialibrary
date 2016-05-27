using System;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Repositories;
using System.Linq.Expressions;

namespace Anatoli.Business.Domain
{
    public class CityRegionDomain : BusinessDomainV3<CityRegion>, IBusinessDomainV3<CityRegion>
    {
        #region Ctors
        public CityRegionDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public CityRegionDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(CityRegion currentCityRegion, CityRegion item)
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

        public override void SetConditionForFetchingData()
        {
            MainRepository.ExtraPredicate = p => true;
        }
        #endregion
    }
}
