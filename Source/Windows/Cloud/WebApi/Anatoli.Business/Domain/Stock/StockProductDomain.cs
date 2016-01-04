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
    public class StockProductDomain : BusinessDomain<StockProductViewModel>, IBusinessDomain<StockProduct, StockProductViewModel>
    {
        #region Properties
        public IAnatoliProxy<StockProduct, StockProductViewModel> Proxy { get; set; }
        public IRepository<StockProduct> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StockProductDomain() { }
        public StockProductDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockProductDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockProductRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<StockProduct, StockProductViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockProductDomain(IStockProductRepository dataRepository, IPrincipalRepository principalRepository, IAnatoliProxy<StockProduct, StockProductViewModel> proxy)
        {
            Proxy = proxy;
            Repository = dataRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StockProductViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task PublishAsync(List<StockProductViewModel> dataViewModels)
        {
            try
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentDataList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                dataList.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentData = currentDataList.Find(p => p.Id == item.Id);
                    if (currentData != null)
                    {
                        if (currentData.MinQty != item.MinQty ||
                            currentData.ReorderLevel != item.ReorderLevel ||
                            currentData.MaxQty != item.MaxQty ||
                            currentData.IsEnable != item.IsEnable ||
                            currentData.StockId != item.StockId ||
                            currentData.FiscalYearId != item.FiscalYearId ||
                            currentData.ProductId != item.ProductId ||
                            currentData.ReorderCalcTypeId != item.ReorderCalcTypeId 
                            )
                        {
                            currentData.MinQty = item.MinQty;
                            currentData.ReorderLevel = item.ReorderLevel;
                            currentData.MaxQty = item.MaxQty;
                            currentData.IsEnable = item.IsEnable;
                            currentData.StockId = item.StockId;
                            currentData.FiscalYearId = item.FiscalYearId;
                            currentData.ProductId = item.ProductId;
                            currentData.ReorderCalcTypeId = item.ReorderCalcTypeId;

                            currentData.LastUpdate = DateTime.Now;
                            Repository.Update(currentData);
                        }
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        Repository.Add(item);
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

        public async Task Delete(List<StockProductViewModel> dataViewModels)
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
