using System;
using System.Linq;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class ProductRateDomain : BusinessDomainV2<ProductRate, ProductRateViewModel, ProductRateRepository, IProductRateRepository>, IBusinessDomainV2<ProductRate, ProductRateViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public ProductRateDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public ProductRateDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<List<ProductRateViewModel>> GetAllAvg()
        {
            var productRates = await MainRepository.FindAllAsync(p => p.DataOwnerId == DataOwnerKey);

            var resulDict = productRates
                        .GroupBy(f => f.ProductId)
                        .Select(g => new { ProductId = g.Key, Avg = g.Average(n => n.Value) })
                        .Select(row => new ProductRateViewModel { Avg = row.Avg, ProductGuid = row.ProductId });
            return resulDict.ToList();
        }

        public async Task<List<ProductRateViewModel>> GetAllAvgChangeAfter(DateTime selectedDate)
        {
            var productRates = await MainRepository.FindAllAsync(p => p.DataOwnerId == DataOwnerKey && p.ApplicationOwnerId == ApplicationOwnerKey);
            var changedProducts = ((AnatoliDbContext)MainRepository.DbContext).ProductRates
                            .Where(p => p.DataOwnerId == DataOwnerKey && p.ApplicationOwnerId == ApplicationOwnerKey && p.LastUpdate >= selectedDate)
                            .Select(m => m.ProductId)
                            .Distinct();
            
            var resultDict = productRates
                        .Where(w => changedProducts.Contains(w.ProductId))
                        .GroupBy(f => f.ProductId)
                        .Select(g => new { ProductId = g.Key, Avg = g.Average(n => n.Value) })
                        .Select(row => new ProductRateViewModel { Avg = row.Avg, ProductGuid = row.ProductId });

            return resultDict.ToList();
        }


        public async Task<List<ProductRate>> GetAllByProductId(string productId)
        {
            Guid productGuid = Guid.Parse(productId);
            var productRates = await MainRepository.FindAllAsync(p => p.Id == productGuid && p.DataOwnerId == DataOwnerKey && p.ApplicationOwnerId == ApplicationOwnerKey);

            return productRates.ToList(); ;
        }

        public async Task<List<ProductRate>> PublishAsyncWithResult(List<ProductRate> ProductRates)
        {
            try
            {
                ProductRates.ForEach(item =>
                {
                    item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;
                    var currentProductRate = MainRepository.GetQuery().Where(p => p.Id == item.Id && p.DataOwnerId == DataOwnerKey).FirstOrDefault();
                    if (currentProductRate != null)
                    {
                        currentProductRate.RateBy = item.RateBy;
                        currentProductRate.RateByName = item.RateByName;
                        currentProductRate.RateDate = item.RateDate;
                        currentProductRate.RateTime = item.RateTime;
                        currentProductRate.Value = item.Value;

                        currentProductRate.ProductId = item.ProductId;
                        MainRepository.Update(item);

                    }
                    else
                    {
                        item.Id = Guid.NewGuid();
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        MainRepository.Add(item);
                    }


                }
                );
                await MainRepository.SaveChangesAsync();

                var productDomain = new ProductDomain(ApplicationOwnerKey, DataOwnerKey, DataOwnerCenterKey);

                var result = new List<ProductRate>();
                foreach (var item in ProductRates)
                {
                    var rateData = await MainRepository.FindAllAsync(p => p.ProductId == item.ProductId && p.DataOwnerId == DataOwnerKey);
                    var resulDict = rateData
                            .GroupBy(f => f.ProductId)
                            .Select(g => new { ProductId = g.Key, Avg = g.Average(n => n.Value) })
                            .Select(row => new ProductRate { Avg = row.Avg, ProductId = row.ProductId });
                    result.AddRange(resulDict.ToList());
                }

                foreach (var item in result)
                {
                    await productDomain.ChangeProductRates(item.ProductId, item.Avg);
                }

                await MainRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("PublishAsync", ex);
                throw ex;
            }

        }

        #endregion
    }
}
