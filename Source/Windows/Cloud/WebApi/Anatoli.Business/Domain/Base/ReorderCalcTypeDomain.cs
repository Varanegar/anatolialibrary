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
using Anatoli.ViewModels.BaseModels;

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
