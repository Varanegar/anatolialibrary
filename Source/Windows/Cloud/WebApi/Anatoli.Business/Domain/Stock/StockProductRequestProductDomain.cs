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
    public class StockProductRequestProductDomain : BusinessDomainV2<StockProductRequestProduct, StockProductRequestProductViewModel, StockProductRequestProductRepository, IStockProductRequestProductRepository>, IBusinessDomainV2<StockProductRequestProduct, StockProductRequestProductViewModel>
    {
        #region Properties
        public IRepository<Stock> StockRepository { get; set; }
        #endregion

        #region Ctors
        public StockProductRequestProductDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockProductRequestProductDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            StockRepository = new StockRepository(dbc);
        }

        #endregion

        #region Methods
        protected override void AddDataToRepository(StockProductRequestProduct currentData, StockProductRequestProduct item)
        {
            if (currentData != null)
            {
                currentData.Accepted1Qty = item.Accepted1Qty;
                currentData.Accepted2Qty = item.Accepted2Qty;
                currentData.Accepted3Qty = item.Accepted3Qty;
                currentData.DeliveredQty = item.DeliveredQty;
                currentData.ProductId = item.ProductId;
                currentData.RequestQty = item.RequestQty;
                currentData.StockProductRequestId = item.StockProductRequestId;

                currentData.LastUpdate = DateTime.Now;
                MainRepository.Update(item);
            }
            else
            {
                item.Id = Guid.NewGuid();
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        public async Task<List<StockProductRequestProductViewModel>> UpdateStockProductRequestProductDetails(List<StockProductRequestProductViewModel> model,
                                                                                                             Guid stockId, string currentUserId)
        {
            foreach (var item in model)
            {
                var stock = await StockRepository.GetByIdAsync(stockId);
                var stockProductRequestProduct = await MainRepository.GetByIdAsync(item.UniqueId);

                if (stock.Accept1ById == currentUserId)
                    stockProductRequestProduct.Accepted1Qty = item.MyAcceptedQty;
                if (stock.Accept2ById == currentUserId)
                    stockProductRequestProduct.Accepted2Qty = item.MyAcceptedQty;
                if (stock.Accept3ById == currentUserId)
                    stockProductRequestProduct.Accepted3Qty = item.MyAcceptedQty;
            }

            await MainRepository.SaveChangesAsync();

            return model;
        }

        public async Task<ICollection<StockProductRequestProduct>> GetDetailsHistory(Guid stockProductRequestId)
        {
            return await MainRepository.FindAllAsync(p => p.StockProductRequestId == stockProductRequestId && p.DataOwnerId == DataOwnerKey);

        }
        #endregion
    }
}
