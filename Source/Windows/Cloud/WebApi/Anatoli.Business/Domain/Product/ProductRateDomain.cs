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

namespace Anatoli.Business.Domain
{
    public class ProductRateDomain : BusinessDomain<ProductRateViewModel>, IBusinessDomain<ProductRate, ProductRateViewModel>
    {
        #region Properties
        public IAnatoliProxy<ProductRate, ProductRateViewModel> Proxy { get; set; }
        public IRepository<ProductRate> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        ProductRateDomain() { }
        public ProductRateDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public ProductRateDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ProductRateRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<ProductRate, ProductRateViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ProductRateDomain(IProductRateRepository ProductRateRepository, IPrincipalRepository principalRepository, IAnatoliProxy<ProductRate, ProductRateViewModel> proxy)
        {
            Proxy = proxy;
            Repository = ProductRateRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ProductRateViewModel>> GetAll()
        {
            var productRates = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(productRates.ToList()); ;
        }

        public async Task<List<ProductRateViewModel>> GetAllAvg()
        {
            var productRates = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            var resulDict = productRates
                        .GroupBy(f => f.ProductId)
                        .Select(g => new { ProductId = g.Key, Avg = g.Average(n => n.Value) })
                        .Select(row => new ProductRateViewModel { Avg = row.Avg, ProductGuid = row.ProductId });
            return resulDict.ToList();
        }

        public async Task<List<ProductRateViewModel>> GetAllAvgChangeAfter(DateTime selectedDate)
        {
            var productRates = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);
            var changedProducts = Repository.DbContext.ProductRates
                            .Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate)
                            .Select(m => m.ProductId)
                            .Distinct();
            Repository.DbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var resulDict = productRates
                        .Where(w => changedProducts.Contains(w.ProductId))
                        .GroupBy(f => f.ProductId)
                        .Select(g => new { ProductId = g.Key, Avg = g.Average(n => n.Value) })
                        .Select(row => new ProductRateViewModel { Avg = row.Avg, ProductGuid = row.ProductId });

            return resulDict.ToList();
        }


        public async Task<List<ProductRateViewModel>> GetAllByProductId(string productId)
        {
            Guid productGuid = Guid.Parse(productId);
            var productRates = await Repository.FindAllAsync(p => p.Id == productGuid);

            return Proxy.Convert(productRates.ToList()); ;
        }

        public async Task<List<ProductRateViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var productRates = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(productRates.ToList()); ;
        }

        public async Task PublishAsync(List<ProductRateViewModel> ProductRateViewModels)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }

        }
        public async Task<List<ProductRateViewModel>> PublishAsyncWithReturn(List<ProductRateViewModel> ProductRateViewModels)
        {
            try
            {
                List<ProductRateViewModel> result = new List<ProductRateViewModel>();
                var ProductRates = Proxy.ReverseConvert(ProductRateViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                ProductRates.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentProductRate = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (currentProductRate != null)
                    {
                        currentProductRate.RateBy = item.RateBy;
                        currentProductRate.RateByName = item.RateByName;
                        currentProductRate.RateDate = item.RateDate;
                        currentProductRate.RateTime = item.RateTime;
                        currentProductRate.Value = item.Value;

                        currentProductRate = SetProductData(currentProductRate, item.Product, Repository.DbContext);
                        Repository.UpdateAsync(item);

                    }
                    else
                    {
                        item.Id = Guid.NewGuid();
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        Repository.AddAsync(item);
                    }


                }
                );

                await Repository.SaveChangesAsync();

                foreach (var item in ProductRates)
                {
                    var rateData = await Repository.FindAllAsync(p => p.ProductId == item.ProductId);
                    var resulDict = rateData
                            .GroupBy(f => f.ProductId)
                            .Select(g => new { ProductId = g.Key, Avg = g.Average(n => n.Value) })
                            .Select(row => new ProductRateViewModel { Avg = row.Avg, ProductGuid = row.ProductId });
                    result.AddRange(resulDict.ToList());
                }

                return result;

            }
            catch (Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }

            await Repository.SaveChangesAsync();
        }

        public async Task Delete(List<ProductRateViewModel> ProductRateViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var ProductRates = Proxy.ReverseConvert(ProductRateViewModels);

                ProductRates.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }

        public ProductRate SetProductData(ProductRate data, Product product, AnatoliDbContext context)
        {
            ProductDomain productDomain = new ProductDomain(data.PrivateLabelOwner.Id, context);
            var result = productDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == product.PrivateLabelOwner.Id && p.Number_ID == product.Number_ID).FirstOrDefault();
            data.Product = result;
            return data;
        }
        #endregion
    }
}
