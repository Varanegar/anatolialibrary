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

namespace Anatoli.Business.Domain
{
    public class StockTypeDomain : BusinessDomain<StockTypeViewModel>, IBusinessDomain<StockType, StockTypeViewModel>
    {
        #region Properties
        public IAnatoliProxy<StockType, StockTypeViewModel> Proxy { get; set; }
        public IRepository<StockType> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StockTypeDomain() { }
        public StockTypeDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockTypeDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockTypeRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<StockType, StockTypeViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockTypeDomain(IStockTypeRepository dataRepository, IPrincipalRepository principalRepository, IAnatoliProxy<StockType, StockTypeViewModel> proxy)
        {
            Proxy = proxy;
            Repository = dataRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StockTypeViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockTypeViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task PublishAsync(List<StockTypeViewModel> dataViewModels)
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
                        if (currentData.StockTypeName != item.StockTypeName)
                        {
                            currentData.StockTypeName = item.StockTypeName;
                            currentData.LastUpdate = DateTime.Now;
                            Repository.UpdateAsync(currentData);
                        }
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

        public async Task Delete(List<StockTypeViewModel> dataViewModels)
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
        #endregion
    }
}
