using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Aantoli.Common.Entity.Gateway.Product;

namespace Anatoli.Business
{
    public class ProductDomain : IBusinessDomain<Product, ProductEntity>
    {
        #region Properties
        public IAnatoliProxy<Product, ProductEntity> Proxy { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }
        #endregion

        #region Ctors
        private ProductDomain() { }
        public ProductDomain(Guid privateLabelOwnerId)
            : this(new ProductRepository(), AnatoliProxy<Product, ProductEntity>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        ProductDomain(IProductRepository productRepository, IAnatoliProxy<Product, ProductEntity> proxy)
        {
            Proxy = proxy;
            ProductRepository = productRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ProductEntity>> GetAll(Guid storeId)
        {
            var products = await ProductRepository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);// && p.store;

            return Proxy.Convert(products.ToList()); ;
        }

        public async Task<List<ProductEntity>> GetAllChangedAfter(Guid storeId, DateTime selectedDate)
        {
            var products = await ProductRepository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(products.ToList()); ;
        }

        public async Task Publish(List<ProductEntity> productEntities)
        {
            throw new NotImplementedException();
        }
        public async Task Delete(List<ProductEntity> productEntities)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
