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
    public class StockProductRequestProductDomain : BusinessDomain<StockProductRequestProductViewModel>, IBusinessDomain<StockProductRequestProduct, StockProductRequestProductViewModel>
    {
        #region Properties
        public IAnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel> Proxy { get; set; }
        public IRepository<StockProductRequestProduct> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StockProductRequestProductDomain() { }
        public StockProductRequestProductDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockProductRequestProductDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockProductRequestProductRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockProductRequestProductDomain(IStockProductRequestProductRepository dataRepository, IPrincipalRepository principalRepository, IAnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel> proxy)
        {
            Proxy = proxy;
            Repository = dataRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StockProductRequestProductViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductRequestProductViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductRequestProductViewModel>> PublishAsync(List<StockProductRequestProductViewModel> dataViewModels)
        {
            try
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                dataList.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentData = Repository.GetQuery().Where(p => p.ProductId == item.ProductId && p.StockProductRequestId == item.StockProductRequestId).FirstOrDefault();
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
                        Repository.UpdateAsync(item);
                    }
                    else
                    {
                        item.Id = Guid.NewGuid();
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
            return dataViewModels;
        }

        public async Task<List<StockProductRequestProductViewModel>> Delete(List<StockProductRequestProductViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);

                dataList.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.StockProductRequestProducts.Remove(data);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }
        #endregion
    }
}
