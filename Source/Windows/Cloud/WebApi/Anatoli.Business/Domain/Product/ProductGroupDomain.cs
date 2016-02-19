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
    public class ProductGroupDomain : BusinessDomain<ProductGroupViewModel>, IBusinessDomain<ProductGroup, ProductGroupViewModel>
    {
        #region Properties
        public IAnatoliProxy<ProductGroup, ProductGroupViewModel> Proxy { get; set; }
        public IRepository<ProductGroup> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        ProductGroupDomain() { }
        public ProductGroupDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public ProductGroupDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ProductGroupRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<ProductGroup, ProductGroupViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ProductGroupDomain(IProductGroupRepository productGroupRepository, IPrincipalRepository principalRepository, IAnatoliProxy<ProductGroup, ProductGroupViewModel> proxy)
        {
            Proxy = proxy;
            Repository = productGroupRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ProductGroupViewModel>> GetAll()
        {
            var productGroups = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(productGroups.ToList()); ;
        }

        public async Task<List<ProductGroupViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var productGroups = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(productGroups.ToList()); ;
        }

        public async Task<List<ProductGroupViewModel>> PublishAsync(List<ProductGroupViewModel> dataViewModels)
        {
            try
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var productGroups = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentGroupList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                foreach (ProductGroup item in productGroups)
                {
                    var currentGroup = currentGroupList.Find(t => t.Id == item.Id);
                    if (currentGroup != null)
                    {
                        if (currentGroup.GroupName != item.GroupName ||
                                currentGroup.NLeft != item.NLeft ||
                                currentGroup.NRight != item.NRight ||
                                currentGroup.NLevel != item.NLevel ||
                                currentGroup.ProductGroup2Id != item.ProductGroup2Id)
                        {

                            currentGroup.LastUpdate = DateTime.Now;
                            currentGroup.GroupName = item.GroupName;
                            currentGroup.NLeft = item.NLeft;
                            currentGroup.NRight = item.NRight;
                            currentGroup.NLevel = item.NLevel;
                            currentGroup.ProductGroup2Id = item.ProductGroup2Id;
                            Repository.Update(currentGroup);
                        }
                    }
                    else
                    {
                        item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        Repository.Add(item);
                    }

                }
                await Repository.SaveChangesAsync();
            }
            catch(Exception ex)
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
        public async Task<List<ProductGroupViewModel>> CheckDeletedAsync(List<ProductGroupViewModel> dataViewModels)
        {
            try
            {
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentDataList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                currentDataList.ForEach(item =>
                {
                    if (dataViewModels.Find(p => p.UniqueId == item.Id) == null)
                    {
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
        public async Task<List<ProductGroupViewModel>> Delete(List<ProductGroupViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var productGroups = Proxy.ReverseConvert(dataViewModels);

                productGroups.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DbContext.ProductGroups.Remove(data);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }
        #endregion
    }
}
