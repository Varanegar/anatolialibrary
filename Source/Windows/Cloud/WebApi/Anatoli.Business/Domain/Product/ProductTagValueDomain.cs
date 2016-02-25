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
    public class ProductTagValueDomain : BusinessDomain<ProductTagValueViewModel>, IBusinessDomain<ProductTagValue, ProductTagValueViewModel>
    {
        #region Properties
        public IAnatoliProxy<ProductTagValue, ProductTagValueViewModel> Proxy { get; set; }
        public IRepository<ProductTagValue> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        ProductTagValueDomain() { }
        public ProductTagValueDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public ProductTagValueDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ProductTagValueRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<ProductTagValue, ProductTagValueViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ProductTagValueDomain(IProductTagValueRepository dataRepository, IPrincipalRepository principalRepository, IAnatoliProxy<ProductTagValue, ProductTagValueViewModel> proxy)
        {
            Proxy = proxy;
            Repository = dataRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ProductTagValueViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<ProductTagValueViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<ProductTagValueViewModel>> PublishAsync(List<ProductTagValueViewModel> dataViewModels)
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
                        if (currentData.ProductId != item.ProductId ||
                            currentData.ProductTagId != item.ProductTagId ||
                            currentData.FromDate != item.FromDate ||
                            currentData.ToDate != item.ToDate
                            )
                        {
                            currentData.ProductId = item.ProductId;
                            currentData.ProductTagId = item.ProductTagId;
                            currentData.FromDate = item.FromDate;
                            currentData.ToDate = item.ToDate;
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
            return dataViewModels;
        }
        public async Task<List<ProductTagValueViewModel>> CheckDeletedAsync(List<ProductTagValueViewModel> dataViewModels)
        {
            try
            {
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentDataList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                currentDataList.ForEach(item =>
                {
                    if (dataViewModels.Find(p => p.UniqueId == item.Id) == null)
                    {
                        item.LastUpdate = DateTime.Now;
                        item.IsRemoved = true;
                        Repository.UpdateAsync(item);
                    }
                });

                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("CheckForDeletedAsync", ex);
                throw ex;
            }

            return dataViewModels;
        }

        public async Task<List<ProductTagValueViewModel>> Delete(List<ProductTagValueViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);

                dataList.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.ProductTagValue.Remove(data);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }
        #endregion
    }
}
