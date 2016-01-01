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

        public async Task PublishAsync(List<ProductGroupViewModel> ProductGroupViewModels)
        {
            try
            {
                var productGroups = Proxy.ReverseConvert(ProductGroupViewModels);
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
        }

        public async Task Delete(List<ProductGroupViewModel> ProductGroupViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var productGroups = Proxy.ReverseConvert(ProductGroupViewModels);

                productGroups.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }
        #endregion
    }
}
