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
using Anatoli.ViewModels.StoreModels;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.PMC.DataAccess.Helpers;

namespace Anatoli.Business.Domain
{
    public class StockActiveOnHandDomain : BusinessDomain<StockActiveOnHandViewModel>, IBusinessDomain<StockActiveOnHand, StockActiveOnHandViewModel>
    {
        #region Properties
        public IAnatoliProxy<StockActiveOnHand, StockActiveOnHandViewModel> Proxy { get; set; }
        public IRepository<StockActiveOnHand> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StockActiveOnHandDomain() { }
        public StockActiveOnHandDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockActiveOnHandDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockActiveOnHandRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<StockActiveOnHand, StockActiveOnHandViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockActiveOnHandDomain(IStockActiveOnHandRepository dataRepository, IPrincipalRepository principalRepository, IAnatoliProxy<StockActiveOnHand, StockActiveOnHandViewModel> proxy)
        {
            Proxy = proxy;
            Repository = dataRepository;
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

        public async Task PublishAsync(List<StockActiveOnHandViewModel> dataViewModels)
        {
            try
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                dataList.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentData = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (currentData != null)
                    {
                        //if (currentData.StockActiveOnHandName != item.StockActiveOnHandName)
                        //{
                        //    currentData.StockActiveOnHandName = item.StockActiveOnHandName;
                        //    currentData.LastUpdate = DateTime.Now;
                        //    Repository.UpdateAsync(currentData);
                        //}
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        Repository.AddAsync(item);
                    }
                });

                await Repository.SaveChangesAsync();
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

        public async Task PublishAsyncOnHandSyncInfo(Guid stockId, Principal privateOwnerId, AnatoliDbContext context)
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

        }
        #endregion
    }
}
