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
    public class StockProductRequestStatusDomain : BusinessDomainV2<StockProductRequestStatus, StockProductRequestStatusViewModel, StockProductRequestStatusRepository, IStockProductRequestStatusRepository>, IBusinessDomainV2<StockProductRequestStatus, StockProductRequestStatusViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StockProductRequestStatusDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockProductRequestStatusDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(StockProductRequestStatus currentData, StockProductRequestStatus item)
        {
            if (currentData != null)
            {
                if (currentData.StockProductRequestStatusName != item.StockProductRequestStatusName)
                {
                    currentData.StockProductRequestStatusName = item.StockProductRequestStatusName;

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
