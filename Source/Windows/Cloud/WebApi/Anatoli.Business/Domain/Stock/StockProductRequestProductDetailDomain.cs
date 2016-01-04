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
    public class StockProductRequestProductDetailDomain : BusinessDomain<StockProductRequestProductDetailViewModel>, IBusinessDomain<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel>
    {
        #region Properties
        public IAnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel> Proxy { get; set; }
        public IRepository<StockProductRequestProductDetail> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StockProductRequestProductDetailDomain() { }
        public StockProductRequestProductDetailDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockProductRequestProductDetailDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockProductRequestProductDetailRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockProductRequestProductDetailDomain(IStockProductRequestProductDetailRepository dataRepository, IPrincipalRepository principalRepository, IAnatoliProxy<StockProductRequestProductDetail, StockProductRequestProductDetailViewModel> proxy)
        {
            Proxy = proxy;
            Repository = dataRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StockProductRequestProductDetailViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductRequestProductDetailViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task PublishAsync(List<StockProductRequestProductDetailViewModel> dataViewModels)
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
                        //if (currentData.StockProductRequestProductDetailName != item.StockProductRequestProductDetailName)
                        //{
                        //    currentData.StockProductRequestProductDetailName = item.StockProductRequestProductDetailName;
                        //    currentData.LastUpdate = DateTime.Now;
                        //    Repository.UpdateAsync(currentData);
                        //}
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

        public async Task Delete(List<StockProductRequestProductDetailViewModel> dataViewModels)
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
