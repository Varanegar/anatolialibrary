using System;
using System.Linq;
using Anatoli.DataAccess;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.StockModels;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.Business.Proxy.Concretes.StockConcretes;
using Anatoli.Business.Proxy.Concretes.StockProductRequestRuleConcretes;

namespace Anatoli.Business.Domain
{
    public class StockDomain : BusinessDomainV2<Stock, StockViewModel, StockRepository, IStockRepository>, IBusinessDomainV2<Stock, StockViewModel>
    {
        #region Properties
        public IRepository<StockProductRequestRule> ProductRequestRuleRepository { get; set; }
        public IRepository<Principal> PrincipalRepository { get; set; }
        #endregion

        #region Ctors
        public StockDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            PrincipalRepository = new PrincipalRepository(dbc);
            ProductRequestRuleRepository = new StockProductRequestRuleRepository(dbc);
        }
        #endregion

        #region Methods
        //public async Task<ICollection<Stock>> GetAllByUserId(Guid userId)
        //{
        //    try
        //    {
        //        var user = await PrincipalRepository.FindAsync(p => p.Id == userId);

        //        return user.Stocks;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("GetAll", ex);
        //        throw ex;
        //    }
        //}

        public async Task<ICollection<Stock>> GetStockCompleteInfo(string stockId)
        {
            try
            {
                Guid stockGuid = Guid.Parse(stockId);
                return await MainRepository.FindAllAsync(p => p.Id == stockGuid);

            }
            catch (Exception ex)
            {
                Logger.Error("GetAll", ex);
                throw ex;
            }
        }

        protected override void AddDataToRepository(Stock currentData, Stock item)
        {
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
                MainRepository.Update(currentData);
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }

        //public async Task SaveStocksUser(Guid userId, List<Guid> stockIds)
        //{
        //    var user = await PrincipalRepository.FindAsync(p => p.Id == userId);
        //    user.Stocks.Clear();

        //    if (stockIds != null)
        //    {
        //        var stocks = await MainRepository.FindAllAsync(p => stockIds.Contains(p.Id));

        //        stocks.ToList().ForEach(itm => user.Stocks.Add(itm));
        //    }

        //    await PrincipalRepository.SaveChangesAsync();
        //}


        #endregion
    }
}
