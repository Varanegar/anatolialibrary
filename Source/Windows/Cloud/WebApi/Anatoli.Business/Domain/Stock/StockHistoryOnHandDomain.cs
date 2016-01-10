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
    public class StockHistoryOnHandDomain : BusinessDomain<StockHistoryOnHandViewModel>, IBusinessDomain<StockHistoryOnHand, StockHistoryOnHandViewModel>
    {
        #region Properties
        public IAnatoliProxy<StockHistoryOnHand, StockHistoryOnHandViewModel> Proxy { get; set; }
        public IRepository<StockHistoryOnHand> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StockHistoryOnHandDomain() { }
        public StockHistoryOnHandDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockHistoryOnHandDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockHistoryOnHandRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<StockHistoryOnHand, StockHistoryOnHandViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockHistoryOnHandDomain(IStockHistoryOnHandRepository dataRepository, IPrincipalRepository principalRepository, IAnatoliProxy<StockHistoryOnHand, StockHistoryOnHandViewModel> proxy)
        {
            Proxy = proxy;
            Repository = dataRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StockHistoryOnHandViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockHistoryOnHandViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockHistoryOnHandViewModel>> PublishAsync(List<StockHistoryOnHandViewModel> dataViewModels)
        {
            try
            {
                throw new NotImplementedException();

            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
        }

        public async Task<List<StockHistoryOnHandViewModel>> Delete(List<StockHistoryOnHandViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);

                dataList.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.StockHistoryOnHands.Remove(data);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }
        #endregion
    }
}
