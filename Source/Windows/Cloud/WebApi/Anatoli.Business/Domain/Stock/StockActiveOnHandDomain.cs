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
    public class StockActiveOnHandDomain : BusinessDomain<StockActiveOnHandViewModel>, IBusinessDomain<StockActiveOnHand, StockActiveOnHandViewModel>
    {
        #region Properties
        public IAnatoliProxy<StockActiveOnHand, StockActiveOnHandViewModel> Proxy { get; set; }
        public IRepository<StockActiveOnHand> Repository { get; set; }
        public IRepository<StockOnHandSync> StockSynRepository { get; set; }
        public IRepository<StockHistoryOnHand> StockHistoryOnHandRepository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StockActiveOnHandDomain() { }
        public StockActiveOnHandDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockActiveOnHandDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockActiveOnHandRepository(dbc), new StockHistoryOnHandRepository(dbc), new StockOnHandSyncRepository(dbc), new PrincipalRepository(dbc), 
                    AnatoliProxy<StockActiveOnHand, StockActiveOnHandViewModel>.Create()
            )
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockActiveOnHandDomain(IStockActiveOnHandRepository dataRepository, IStockHistoryOnHandRepository dataHistoryRepository, IStockOnHandSyncRepository dataSyncRepository, 
                    IPrincipalRepository principalRepository, IAnatoliProxy<StockActiveOnHand, StockActiveOnHandViewModel> proxy)
        {
            Proxy = proxy;
            Repository = dataRepository;
            StockSynRepository = dataSyncRepository;
            StockHistoryOnHandRepository = dataHistoryRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StockActiveOnHandViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockActiveOnHandViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }
        public async Task<List<StockActiveOnHandViewModel>> GetAllByStockId(string stockId)
        {
            Guid stockGuid = Guid.Parse(stockId);
            var stockActiveOnhands = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.StockId == stockGuid);

            return Proxy.Convert(stockActiveOnhands.ToList());
        }


        public async Task PublishAsync(List<StockActiveOnHandViewModel> dataViewModels)
        {
            try
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var syncId = await PublishAsyncOnHandSyncInfo(dataList[0].StockId, privateLabelOwner, Repository.DbContext);
                
                dataList.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    item.StockOnHandSyncId = syncId;
                    item.CreatedDate = item.LastUpdate = DateTime.Now;
                    Repository.AddAsync(item);
                });
                await Repository.SaveChangesAsync();
                SaveActiveInfoIntoHistory(Repository.DbContext);
            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
        }

        public async Task Delete(List<StockActiveOnHandViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);

                dataList.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DeleteAsync(data);
                });

                Repository.SaveChangesAsync();
            });
        }

        public async Task<Guid> PublishAsyncOnHandSyncInfo(Guid stockId, Principal privateOwnerId, AnatoliDbContext context)
        {
            var data = new StockOnHandSync
            {
                CreatedDate = DateTime.Now,
                LastUpdate = DateTime.Now,
                PrivateLabelOwner = privateOwnerId,
                Id = Guid.NewGuid(),
                StockId = stockId,
                SyncDate = DateTime.Now,
                SyncPDate = PersianDate.Today.ToShortDateString(),
            };

            await StockSynRepository.AddAsync(data);

            return data.Id;

        }

        public void SaveActiveInfoIntoHistory(AnatoliDbContext context)
        {
            context.Database.ExecuteSqlCommand(@"insert into StockHistoryOnHands
                SELECT NewID()
                      ,[Qty]
                      ,[StockId]
                      ,[ProductId]
                      ,[StockOnHandSyncId]
                      ,[Number_ID]
                      ,[CreatedDate]
                      ,[LastUpdate]
                      ,[IsRemoved]
                      ,[AddedBy_Id]
                      ,[LastModifiedBy_Id]
                      ,[PrivateLabelOwner_Id]
                  FROM [dbo].[StockActiveOnHands]
                ");
        }
        #endregion
    }
}
