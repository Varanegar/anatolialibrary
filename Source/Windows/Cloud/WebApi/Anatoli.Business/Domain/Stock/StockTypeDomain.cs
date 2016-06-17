using System;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.StockModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class StockTypeDomain : BusinessDomainV2<StockType, StockTypeViewModel, StockTypeRepository, IStockTypeRepository>, IBusinessDomainV2<StockType, StockTypeViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StockTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(StockType currentData, StockType item)
        {
            if (currentData != null)
            {
                if (currentData.StockTypeName != item.StockTypeName)
                {
                    currentData.StockTypeName = item.StockTypeName;
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
