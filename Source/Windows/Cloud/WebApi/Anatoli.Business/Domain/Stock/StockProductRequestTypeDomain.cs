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
    public class StockProductRequestTypeDomain : BusinessDomainV2<StockProductRequestType, StockProductRequestTypeViewModel, StockProductRequestTypeRepository, IStockProductRequestTypeRepository>, IBusinessDomainV2<StockProductRequestType, StockProductRequestTypeViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StockProductRequestTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockProductRequestTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(StockProductRequestType currentData, StockProductRequestType item)
        {
            if (currentData != null)
            {
                if (currentData.StockProductRequestTypeName != item.StockProductRequestTypeName)
                {
                    currentData.StockProductRequestTypeName = item.StockProductRequestTypeName;
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
