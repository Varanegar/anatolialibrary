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

        public async Task<List<StockProductViewModel>> GetAllByStockId(string stockId)
        {
            Guid stockGuid = Guid.Parse(stockId);
            var dataList = await Repository.FindAllAsync(p => p.StockId == stockGuid);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductViewModel>> PublishAsync(List<StockProductViewModel> dataViewModels)
        {
            try
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var dataList = Proxy.ReverseConvert(dataViewModels);

                var currentDataList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                dataList.ForEach(item =>
                {
                    var data = currentDataList.Find(p => p.Id == item.Id);
                    if (data != null)
                    {
                        data.MinQty = item.MinQty;
                        data.ReorderLevel = item.ReorderLevel;
                        data.MaxQty = item.MaxQty;
                        data.IsEnable = item.IsEnable;
                        data.StockId = item.StockId;
                        data.FiscalYearId = item.FiscalYearId;
                        data.ProductId = item.ProductId;

                        data.LastUpdate = DateTime.Now;

                        if (item.ReorderCalcType != null && item.ReorderCalcType.Id != Guid.Empty)
                            data.ReorderCalcTypeId = item.ReorderCalcType.Id;

                        Repository.Update(data);
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;

                        item.PrivateLabelOwner_Id = PrivateLabelOwnerId;

                        Repository.Add(item);
                    }
                });

                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            finally
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                log.Info("PublishAsync Finish" + dataViewModels.Count);
            }
            return dataViewModels;
        }

        public async Task<List<StockProductViewModel>> Delete(List<StockProductViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);

                dataList.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.StockProducts.Remove(data);
                });

                Repository.SaveChangesAsync();
            });

            return dataViewModels;
        }
        #endregion
    }
}
