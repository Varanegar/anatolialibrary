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
    public class ProductDomain : IBusinessDomain<Product, ProductViewModel>
    {
        #region Properties
        public IAnatoliProxy<Product, ProductViewModel> Proxy { get; set; }
        public IRepository<Product> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        ProductDomain() { }
        public ProductDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public ProductDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ProductRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Product, ProductViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ProductDomain(IProductRepository productRepository, IPrincipalRepository principalRepository, IAnatoliProxy<Product, ProductViewModel> proxy)
        {
            Proxy = proxy;
            Repository = productRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ProductViewModel>> GetAll()
        {
            var products = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(products.ToList()); ;
        }

        public async Task<List<ProductViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var products = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(products.ToList()); ;
        }

        public async Task PublishAsync(List<ProductViewModel> ProductViewModels)
        {
            var products = Proxy.ReverseConvert(ProductViewModels);
            var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

            products.ForEach(item =>
            {
                var currentProduct = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();

                if (currentProduct != null)
                {
                    currentProduct.ProductName = item.ProductName;
                    Repository.UpdateAsync(SetCharTypeData(currentGroup, item.CharTypes.ToList(), Repository.DbContext));
                }
                else
                {
                    item.Suppliers.ToList().ForEach(itm =>
                    {
                        itm.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                        itm.CreatedDate = itm.LastUpdate = DateTime.Now;
                    });

                    item.PrivateLabelOwner = item.Manufacture.PrivateLabelOwner = item.ProductGroup.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;

                    item.CreatedDate = item.LastUpdate = item.Manufacture.CreatedDate =
                    item.Manufacture.LastUpdate = item.ProductGroup.CreatedDate = item.ProductGroup.LastUpdate = DateTime.Now;

                    item.ProductGroup.ProductGroup2 = null;

                    Repository.AddAsync(item);
                }
            });
            await Repository.SaveChangesAsync();
        }

        public async Task Delete(List<ProductViewModel> ProductViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var products = Proxy.ReverseConvert(ProductViewModels);

                products.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }

        public CharGroup SetCharTypeData(CharGroup data, List<CharType> charTypes, AnatoliDbContext context)
        {
            CharTypeDomain charTypeDomain = new CharTypeDomain(data.PrivateLabelOwner.Id, context);
            data.CharTypes.Clear();
            charTypes.ForEach(item =>
            {
                var charType = charTypeDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (charType != null)
                    data.CharTypes.Add(charType);
            });
            return data;
        }
        #endregion
    }
}
