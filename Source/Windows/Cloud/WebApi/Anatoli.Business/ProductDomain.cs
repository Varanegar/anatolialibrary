using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.ViewModels.Product;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;

namespace Anatoli.Business
{
    public class ProductDomain : IBusinessDomain<Product, ProductViewModel>
    {
        #region Properties
        public IAnatoliProxy<Product, ProductViewModel> Proxy { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }
        #endregion

        #region Ctors
        private ProductDomain() { }
        public ProductDomain(Guid privateLabelOwnerId)
            : this(new ProductRepository(), AnatoliProxy<Product, ProductViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ProductDomain(IProductRepository productRepository, IAnatoliProxy<Product, ProductViewModel> proxy)
        {
            Proxy = proxy;
            ProductRepository = productRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ProductViewModel>> GetAll()
        {
            var products = await ProductRepository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(products.ToList()); ;
        }

        public async Task<List<ProductViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var products = await ProductRepository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(products.ToList()); ;
        }

        public async Task Publish(List<ProductViewModel> ProductViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var products = Proxy.ReverseConvert(ProductViewModels);

                products.ForEach(item =>
                {
                    var product = ProductRepository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                    if (product != null)
                        ProductRepository.UpdateAsync(product);
                    else
                        ProductRepository.AddAsync(item);
                });

                ProductRepository.SaveChangesAsync();
            });
        }
        public async Task Delete(List<ProductViewModel> ProductViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var products = Proxy.ReverseConvert(ProductViewModels);

                products.ForEach(item =>
                {
                    var product = ProductRepository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                    ProductRepository.DeleteAsync(product);
                });
                ProductRepository.SaveChangesAsync();

            });
        }
        #endregion
    }
}
