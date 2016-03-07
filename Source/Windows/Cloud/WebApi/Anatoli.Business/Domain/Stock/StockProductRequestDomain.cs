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
    public class StockProductRequestDomain : BusinessDomain<StockProductRequestViewModel>, IBusinessDomain<StockProductRequest, StockProductRequestViewModel>
    {
        #region Properties
        public IAnatoliProxy<StockProductRequest, StockProductRequestViewModel> Proxy { get; set; }
        public IAnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel> StockProductRequestProductProxy { get; set; }
        public IAnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel> StockProductRequestProductDetailProxy { get; set; }

        public IRepository<StockProductRequest> Repository { get; set; }
        public IRepository<Stock> StockRepository { get; set; }
        public IRepository<StockProductRequestProduct> StockProductRequestProductRepository { get; set; }
        public IRepository<StockProductRequestProductDetail> StockProductRequestProductDetailRepository { get; set; }
        #endregion

        #region Ctors
        StockProductRequestDomain() { }
        public StockProductRequestDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockProductRequestDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockProductRequestRepository(dbc),
                  new PrincipalRepository(dbc),
                  new StockProductRequestProductRepository(dbc),
                  new StockProductRequestProductDetailRepository(dbc),
                  new StockRepository(dbc),
                  AnatoliProxy<StockProductRequest, StockProductRequestViewModel>.Create(),
                  AnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel>.Create(),
                  AnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockProductRequestDomain(IStockProductRequestRepository repository,
                                         IPrincipalRepository principalRepository,
                                         IRepository<StockProductRequestProduct> stockProductRequestProductRepository,
                                         IRepository<StockProductRequestProductDetail> stockProductRequestProductDetailRepository,
                                         IRepository<Stock> stockRepository,
                                         IAnatoliProxy<StockProductRequest, StockProductRequestViewModel> proxy,
                                         IAnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel> stockProductRequestProductProxy,
                                         IAnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel> stockProductRequestProductDetailProxy
                                        )
        {
            Proxy = proxy;
            StockProductRequestProductProxy = stockProductRequestProductProxy;
            StockProductRequestProductDetailProxy = stockProductRequestProductDetailProxy;

            Repository = repository;
            StockRepository = stockRepository;
            PrincipalRepository = principalRepository;
            StockProductRequestProductRepository = stockProductRequestProductRepository;
            StockProductRequestProductDetailRepository = stockProductRequestProductDetailRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StockProductRequestViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductRequestViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductRequestViewModel>> PublishAsync(List<StockProductRequestViewModel> dataViewModels)
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
                        throw new NotImplementedException();
                    }
                    else
                    {
                        throw new NotImplementedException();

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

        public async Task<List<StockProductRequestViewModel>> Delete(List<StockProductRequestViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);

                dataList.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.StockProductRequests.Remove(data);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }

        public async Task<List<StockProductRequestViewModel>> GetHistory(Guid stockId)
        {
            var model = await Repository.GetFromCachedAsync(p => stockId == Guid.Empty || p.StockId == stockId);

            return Proxy.Convert(model.ToList());
        }

        public async Task<List<StockProductRequestProductViewModel>> GetDetailsHistory(Guid stockProductRequestId)
        {
            var model = await StockProductRequestProductRepository.GetFromCachedAsync(p => p.StockProductRequestId == stockProductRequestId);

            return StockProductRequestProductProxy.Convert(model.ToList());
        }

        public async Task<List<StockProductRequestViewModel>> GetStockProductRequests(string searchTerm)
        {
            var model = await Repository.GetFromCachedAsync(p => string.IsNullOrEmpty(searchTerm) ||
                                                            p.Stock.StockName.Contains(searchTerm) ||
                                                            p.SourceStockRequestNo.Contains(searchTerm));

            return Proxy.Convert(model.ToList());
        }

        public async Task<List<StockProductRequestProductDetailViewModel>> GetStockProductRequestProductDetails(Guid stockProductRequestProductId)
        {
            var model = await StockProductRequestProductDetailRepository.GetFromCachedAsync(p => p.StockProductRequestProductId == stockProductRequestProductId);

            return StockProductRequestProductDetailProxy.Convert(model.ToList());
        }

        public async Task<List<StockProductRequestProductViewModel>> UpdateStockProductRequestProductDetails(List<StockProductRequestProductViewModel> model,
                                                                                                             Guid stockId, Guid currentUserId)
        {
            foreach (var item in model)
            {
                var stock = await StockRepository.GetByIdAsync(stockId);
                var stockProductRequestProduct = await StockProductRequestProductRepository.GetByIdAsync(item.UniqueId);

                if (stock.Accept1ById == currentUserId)
                    stockProductRequestProduct.Accepted1Qty = item.MyAcceptedQty;
                if (stock.Accept2ById == currentUserId)
                    stockProductRequestProduct.Accepted2Qty = item.MyAcceptedQty;
                if (stock.Accept3ById == currentUserId)
                    stockProductRequestProduct.Accepted3Qty = item.MyAcceptedQty;
            }

            await StockProductRequestProductRepository.SaveChangesAsync();

            return model;
        }
        #endregion
    }
}
