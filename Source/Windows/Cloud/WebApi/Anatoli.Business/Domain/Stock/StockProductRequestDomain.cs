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

namespace Anatoli.Business.Domain
{
    public class StockProductRequestDomain : BusinessDomainV2<StockProductRequest, StockProductRequestViewModel, StockProductRequestRepository, IStockProductRequestRepository>, IBusinessDomainV2<StockProductRequest, StockProductRequestViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StockProductRequestDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockProductRequestDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(StockProductRequest currentData, StockProductRequest item)
        {
            if (currentData != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
    
        public async Task<ICollection<StockProductRequest>> GetHistory(Guid stockId)
        {
            return await MainRepository.FindAllAsync(p => (stockId == Guid.Empty || p.StockId == stockId) && p.DataOwnerId == DataOwnerKey);
        }



        public async Task<ICollection<StockProductRequest>> GetStockProductRequests(string searchTerm,string userId)
        {
            return await MainRepository.FindAllAsync(p => (string.IsNullOrEmpty(searchTerm) ||
                                                            p.Stock.StockName.Contains(searchTerm) ||
                                                            p.SourceStockRequestNo.Contains(searchTerm))&&(
                                                            p.Stock.Accept1ById==userId ||
                                                            p.Stock.Accept2ById == userId ||
                                                            p.Stock.Accept3ById == userId 
                                                            ));

        }


        #endregion
    }
}
