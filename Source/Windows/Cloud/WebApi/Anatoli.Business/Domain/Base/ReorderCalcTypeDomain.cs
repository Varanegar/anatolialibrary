using System;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.BaseModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class ReorderCalcTypeDomain : BusinessDomainV2<ReorderCalcType, ReorderCalcTypeViewModel, ReorderCalcTypeRepository, IReorderCalcTypeRepository>, IBusinessDomainV2<ReorderCalcType, ReorderCalcTypeViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public ReorderCalcTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public ReorderCalcTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(ReorderCalcType currentData, ReorderCalcType item)
        {
            if (currentData != null)
            {
                if (currentData.ReorderTypeName != item.ReorderTypeName)
                {
                    currentData.ReorderTypeName = item.ReorderTypeName;
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
