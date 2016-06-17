using System;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.StockModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class StockProductRequestProductDetailDomain : BusinessDomainV2<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel, StockProductRequestProductDetailRepository, IStockProductRequestProductDetailRepository>, IBusinessDomainV2<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StockProductRequestProductDetailDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockProductRequestProductDetailDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }

        #endregion

        #region Methods

        public override async Task PublishAsync(List<StockProductRequestProductDetail> dataViewModels)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                Logger.Error("PublishAsync", ex);
                throw ex;
            }
        }

        public async Task<ICollection<StockProductRequestProductDetail>> GetStockProductRequestProductDetails(Guid stockProductRequestProductId)
        {
            return await MainRepository.FindAllAsync(p => p.StockProductRequestProductId == stockProductRequestProductId && p.DataOwnerId == DataOwnerKey);
        }

        #endregion
    }
}
