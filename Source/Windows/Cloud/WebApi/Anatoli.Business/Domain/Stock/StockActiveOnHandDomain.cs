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
using Anatoli.DataAccess.Models.Identity;
using Anatoli.PMC.DataAccess.Helpers;

namespace Anatoli.Business.Domain
{
    public class StockActiveOnHandDomain : BusinessDomainV2<StockActiveOnHand, StockActiveOnHandViewModel, StockActiveOnHandRepository, IStockActiveOnHandRepository>, IBusinessDomainV2<StockActiveOnHand, StockActiveOnHandViewModel>
    {
        #region Properties
        public IRepository<StockOnHandSync> StockSynRepository { get; set; }
        public IRepository<StockHistoryOnHand> StockHistoryOnHandRepository { get; set; }

        #endregion

        #region Ctors
        public StockActiveOnHandDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockActiveOnHandDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            StockSynRepository = new StockOnHandSyncRepository(dbc);
            StockHistoryOnHandRepository = new StockHistoryOnHandRepository(dbc);
        }
        #endregion

        #region Methods
        public async Task<ICollection<StockActiveOnHandViewModel>> GetAllByStockId(Guid stockId)
        {
            return await GetAllAsync(p => p.DataOwnerId == DataOwnerKey && p.StockId == stockId);

        }


        public override async Task PublishAsync(List<StockActiveOnHand> dataList)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;
                if (dataList.Count == 0) return;
                Guid stockId = dataList[0].StockId;
                await MainRepository.DeleteBatchAsync(p => p.StockId == stockId && p.DataOwnerId == DataOwnerKey);

                var syncId = await PublishAsyncOnHandSyncInfo(dataList[0].StockId, DBContext);
                
                dataList.ForEach(item =>
                {
                    item.Id = Guid.NewGuid();
                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                    item.StockOnHandSyncId = syncId;
                    item.CreatedDate = item.LastUpdate = DateTime.Now;
                    MainRepository.Add(item);
                });
                await MainRepository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Logger.Error("PublishAsync", ex);
                throw ex;
            }
            finally
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                Logger.Info("PublishAsync Finish" + dataList.Count);
            }
        }

        public async Task<Guid> PublishAsyncOnHandSyncInfo(Guid stockId, AnatoliDbContext context)
        {

            try
            {
                var data = new StockOnHandSync
                {
                    CreatedDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    ApplicationOwnerId = ApplicationOwnerKey,
                    DataOwnerId = DataOwnerKey,
                    DataOwnerCenterId = DataOwnerCenterKey,
                    Id = Guid.NewGuid(),
                    StockId = stockId,
                    SyncDate = DateTime.Now,
                    SyncPDate = PersianDate.Today.ToShortDateString(),
                };

                await StockSynRepository.AddAsync(data);
                await MainRepository.SaveChangesAsync();

                return data.Id;
            }
            catch (Exception ex)
            {
                Logger.Error("PublishAsyncOnHandSyncInfo", ex);
                throw ex;
            }
            finally
            {
            }

        }

        #endregion
    }
}
