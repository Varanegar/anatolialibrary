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
        public IRepository<Supplier> SupplierRepository { get; set; }
        public IRepository<CharValue> CharValueRepository { get; set; }
        public IRepository<ProductGroup> ProductGroupRepository { get; set; }
        public IRepository<Manufacture> ManufactureRepository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        ProductDomain() { }
        public ProductDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public ProductDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ProductRepository(dbc), new ProductGroupRepository(dbc), new ManufactureRepository(dbc), new SupplierRepository(dbc), new CharValueRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Product, ProductViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ProductDomain(IProductRepository productRepository, IProductGroupRepository productGroupRepository, IManufactureRepository manufactureRepository, ISupplierRepository supplierRepository, ICharValueRepository charValueRepository, IPrincipalRepository principalRepository, IAnatoliProxy<Product, ProductViewModel> proxy)
        {
            Proxy = proxy;
            Repository = productRepository;
            ProductGroupRepository = productGroupRepository;
            ManufactureRepository = manufactureRepository;
            CharValueRepository = charValueRepository;
            SupplierRepository = supplierRepository;
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
            try
            {
                var products = Proxy.ReverseConvert(ProductViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentProductList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                products.ForEach(item =>
                {
                    var currentProduct = currentProductList.Find(p => p.Id == item.Id);

                    if (currentProduct != null)
                    {
                        currentProduct.ProductName = item.ProductName;
                        currentProduct.ProductGroupId = item.ProductGroupId;
                        currentProduct.ManufactureId = item.ManufactureId;
                        currentProduct.Desctription = item.Desctription;
                        currentProduct.PackUnitValueId = item.PackUnitValueId;
                        currentProduct.PackVolume = item.PackVolume;
                        currentProduct.PackWeight = item.PackWeight;
                        currentProduct.ProductCode = item.ProductCode;
                        currentProduct.ProductName = item.ProductName;
                        currentProduct.ProductTypeValueId = item.ProductTypeValueId;
                        currentProduct.StoreProductName = item.StoreProductName;
                        currentProduct.TaxCategoryValueId = item.TaxCategoryValueId;

                        currentProduct = SetCharValueData(currentProduct, item.CharValues.ToList(), Repository.DbContext);
                        currentProduct = SetSupplierData(currentProduct, item.Suppliers.ToList(), Repository.DbContext);
                        currentProduct = SetMainSupplierData(currentProduct, item.MainSupplier, Repository.DbContext);
                        currentProduct = SetProductPictureData(currentProduct, item.ProductPictures.ToList(), Repository.DbContext);
                        Repository.Update(currentProduct);
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

                        Repository.Add(item);
                    }
                });
                await Repository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
            if (productGroup == null) return data;
            ProductGroupDomain productGroupDomain = new ProductGroupDomain(data.PrivateLabelOwner.Id, context);
            var result = productGroupDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == productGroup.PrivateLabelOwner.Id && p.Number_ID == productGroup.Number_ID).FirstOrDefault();
            data.ProductGroup = result;
            return data;
        }

        public Product SetProductPictureData(Product data, List<ProductPicture> productPictures, AnatoliDbContext context)
        {
            if (productPictures == null) return data;
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
            if (manufacture == null) return data;
            ManufactureDomain manufactureDomain = new ManufactureDomain(data.PrivateLabelOwner.Id, context);
            var result = manufactureDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == manufacture.PrivateLabelOwner.Id && p.Number_ID == manufacture.Number_ID).FirstOrDefault();
            data.Manufacture = result;
            return data;
        }

        public Product SetMainSupplierData(Product data, Supplier suppliers, AnatoliDbContext context)
        {
            if (suppliers == null) return data;
            SupplierDomain supplierDomain = new SupplierDomain(data.PrivateLabelOwner.Id, context);
            var supplier = supplierDomain.Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == suppliers.Number_ID).FirstOrDefault();
            data.MainSupplier = supplier;
            return data;
        }

        public Product SetSupplierData(Product data, List<Supplier> suppliers, AnatoliDbContext context)
        {
            if (suppliers == null) return data;
            Repository.DbContext.Database.ExecuteSqlCommand("delete from ProductSupliers where ProductId=" + data.Id);
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
            if (charValues == null) return data;
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
