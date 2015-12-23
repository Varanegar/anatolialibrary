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
                    currentProduct = SetCharValueData(currentProduct, item.CharValues.ToList(), Repository.DbContext);
                    currentProduct = SetSupplierData(currentProduct, item.Suppliers.ToList(), Repository.DbContext);
                    currentProduct = SetMainSupplierData(currentProduct, item.MainSupplier, Repository.DbContext);
                    currentProduct = SetManucfatureData(currentProduct, item.Manufacture, Repository.DbContext);
                    currentProduct = SetProductGroupData(currentProduct, item.ProductGroup, Repository.DbContext);
                    currentProduct = SetProductPictureData(currentProduct, item.ProductPictures.ToList(), Repository.DbContext);
                    Repository.UpdateAsync(currentProduct);
                }
                else
                {
                    item = SetCharValueData(item, item.CharValues.ToList(), Repository.DbContext);
                    item = SetSupplierData(item, item.Suppliers.ToList(), Repository.DbContext);
                    item = SetMainSupplierData(item, item.MainSupplier, Repository.DbContext);
                    item = SetManucfatureData(item, item.Manufacture, Repository.DbContext);
                    item = SetProductGroupData(item, item.ProductGroup, Repository.DbContext);
                    item = SetProductPictureData(item, item.ProductPictures.ToList(), Repository.DbContext);

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

        public Product SetProductGroupData(Product data, ProductGroup productGroup, AnatoliDbContext context)
        {
            ProductGroupDomain productGroupDomain = new ProductGroupDomain(data.PrivateLabelOwner.Id, context);
            var result = productGroupDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == productGroup.PrivateLabelOwner.Id && p.Number_ID == productGroup.Number_ID).FirstOrDefault();
            data.ProductGroup = result;
            return data;
        }

        public Product SetProductPictureData(Product data, List<ProductPicture> productPictures, AnatoliDbContext context)
        {
            data.ProductPictures.Clear();
            productPictures.ForEach(item =>
            {
                item.PrivateLabelOwner = data.PrivateLabelOwner;
                item.CreatedDate = item.LastUpdate = data.CreatedDate;
                data.ProductPictures.Add(item);
            });
            return data;
        }

        public Product SetManucfatureData(Product data, Manufacture manufacture, AnatoliDbContext context)
        {
            ManufactureDomain manufactureDomain = new ManufactureDomain(data.PrivateLabelOwner.Id, context);
            var result = manufactureDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == manufacture.PrivateLabelOwner.Id && p.Number_ID == manufacture.Number_ID).FirstOrDefault();
            data.Manufacture = result;
            return data;
        }

        public Product SetMainSupplierData(Product data, Supplier suppliers, AnatoliDbContext context)
        {
            SupplierDomain supplierDomain = new SupplierDomain(data.PrivateLabelOwner.Id, context);
            var supplier = supplierDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == suppliers.Number_ID).FirstOrDefault();
            data.MainSupplier = supplier;
            return data;
        }

        public Product SetSupplierData(Product data, List<Supplier> suppliers, AnatoliDbContext context)
        {
            SupplierDomain supplierDomain = new SupplierDomain(data.PrivateLabelOwner.Id, context);
            data.Suppliers.Clear();
            suppliers.ForEach(item =>
            {
                var supplier = supplierDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (supplier != null)
                    data.Suppliers.Add(supplier);
            });
            return data;
        }

        public Product SetCharValueData(Product data, List<CharValue> charValues, AnatoliDbContext context)
        {
            CharValueDomain charTypeDomain = new CharValueDomain(data.PrivateLabelOwner.Id, context);
            data.CharValues.Clear();
            charValues.ForEach(item =>
            {
                var charType = charTypeDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (charType != null)
                    data.CharValues.Add(charType);
            });
            return data;
        }
        #endregion
    }
}
