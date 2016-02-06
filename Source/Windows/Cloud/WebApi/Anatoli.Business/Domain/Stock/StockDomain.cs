using System;
using System.Linq;
using Anatoli.DataAccess;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.StockModels;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.Business.Proxy.Concretes.StockConcretes;

namespace Anatoli.Business.Domain
{
    public class StockDomain : BusinessDomain<StockViewModel>, IBusinessDomain<Stock, StockViewModel>
    {
        #region Properties
        public IAnatoliProxy<Stock, StockViewModel> Proxy { get; set; }
        public IAnatoliProxy<Stock, StockViewModel> ProxyCompleteInfo { get; set; }
        public IRepository<Stock> Repository { get; set; }
        //  public IPrincipalRepository PrincipalRepository { get; set; }
        public IRepository<User> UserRepository { get; set; }
        //   public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StockDomain() { }
        public StockDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockRepository(dbc), new PrincipalRepository(dbc), new UserRepository(dbc), new StockProxy(), new StockCompleteInfoProxy())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockDomain(IStockRepository dataRepository,
                           IPrincipalRepository principalRepository,
                           IUserRepository userRepository,
                           IAnatoliProxy<Stock, StockViewModel> proxy,
                           IAnatoliProxy<Stock, StockViewModel> completeProxy)
        {
            Proxy = proxy;
            ProxyCompleteInfo = completeProxy;
            Repository = dataRepository;
            PrincipalRepository = principalRepository;
            UserRepository = userRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StockViewModel>> GetAll()
        {
            try
            {
                var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

                return Proxy.Convert(dataList.ToList()); ;
            }
            catch (Exception ex)
            {
                log.Error("GetAll", ex);
                throw ex;
            }
        }
        public async Task<List<StockViewModel>> GetAllByUserId(string userId)
        {
            try
            {
                var user = await UserRepository.FindAsync(p => p.Id == userId);

                return Proxy.Convert(user.Stocks.ToList());
            }
            catch (Exception ex)
            {
                log.Error("GetAll", ex);
                throw ex;
            }
        }

        public async Task<List<StockViewModel>> GetStockCompleteInfo(string stockId)
        {
            try
            {
                Guid stockGuid = Guid.Parse(stockId);
                var dataList = await Repository.FindAllAsync(p => p.Id == stockGuid);

                return ProxyCompleteInfo.Convert(dataList.ToList()); ;
            }
            catch (Exception ex)
            {
                log.Error("GetAll", ex);
                throw ex;
            }
        }

        public async Task<List<StockViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockViewModel>> PublishAsync(List<StockViewModel> dataViewModels)
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
                        currentData.Accept1By = item.Accept1By;
                        currentData.Accept1ById = item.Accept1ById;
                        currentData.Accept2ById = item.Accept2ById;
                        currentData.Accept3ById = item.Accept3ById;
                        currentData.Address = item.Address;
                        currentData.StockCode = item.StockCode;
                        currentData.StockName = item.StockName;
                        currentData.StockTypeId = item.StockTypeId;
                        currentData.StoreId = item.StoreId;
                        currentData.MainSCMStock2Id = item.MainSCMStock2Id;
                        currentData.RelatedSCMStock2Id = item.RelatedSCMStock2Id;
                        currentData.LastUpdate = DateTime.Now;
                        Repository.UpdateAsync(currentData);
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        Repository.AddAsync(item);
                    }
                });

                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }

            return dataViewModels;
        }

        public async Task SaveStocksUser(string userId, List<Guid> stockIds)
        {
            var user = await UserRepository.FindAsync(p => p.Id == userId);
            user.Stocks.Clear();

            if (stockIds != null)
            {
                var stocks = await Repository.FindAllAsync(p => stockIds.Contains(p.Id));

                stocks.ToList().ForEach(itm => user.Stocks.Add(itm));
            }

            await UserRepository.SaveChangesAsync();
        }

        public async Task<List<StockViewModel>> Delete(List<StockViewModel> dataViewModels)
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
            return dataViewModels;
        }
        #endregion
    }
}
