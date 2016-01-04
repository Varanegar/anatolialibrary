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
    public class ProductDomain : BusinessDomain<ProductViewModel>, IBusinessDomain<Product, ProductViewModel>
    {
        #region Properties
        public IAnatoliProxy<Product, ProductViewModel> Proxy { get; set; }
        public IRepository<Product> Repository { get; set; }
        public IRepository<Supplier> SupplierRepository { get; set; }
        public IRepository<CharValue> CharValueRepository { get; set; }
        public IRepository<ProductGroup> ProductGroupRepository { get; set; }
        public IRepository<MainProductGroup> MainProductGroupRepository { get; set; }
        public IRepository<Manufacture> ManufactureRepository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        ProductDomain() { }
        public ProductDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public ProductDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new ProductRepository(dbc), new ProductGroupRepository(dbc), new MainProductGroupRepository(dbc), new ManufactureRepository(dbc), new SupplierRepository(dbc), new CharValueRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Product, ProductViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public ProductDomain(IProductRepository productRepository, IProductGroupRepository productGroupRepository, IMainProductGroupRepository mainProductGroupRepository, IManufactureRepository manufactureRepository, ISupplierRepository supplierRepository, ICharValueRepository charValueRepository, IPrincipalRepository principalRepository, IAnatoliProxy<Product, ProductViewModel> proxy)
        {
            Proxy = proxy;
            Repository = productRepository;
            ProductGroupRepository = productGroupRepository;
            MainProductGroupRepository = mainProductGroupRepository;
            ManufactureRepository = manufactureRepository;
            CharValueRepository = charValueRepository;
            SupplierRepository = supplierRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ProductViewModel>> GetAll()
        {
            try
            {
                var products = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

                return Proxy.Convert(products.ToList()); ;
            }
            catch (Exception ex)
            {
                log.Error("GetAll ", ex);
            }
            return null;
        }

        public async Task<List<ProductViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var products = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(products.ToList()); ;
        }

        public async Task PublishAsync(List<ProductViewModel> ProductViewModels)
        {
            log.Info("PublishAsync " + ProductViewModels.Count);
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
                        currentProduct.MainSuppliereId = item.MainSuppliereId;
                        currentProduct.Desctription = item.Desctription;
                        currentProduct.QtyPerPack = item.QtyPerPack;
                        currentProduct.PackVolume = item.PackVolume;
                        currentProduct.PackWeight = item.PackWeight;
                        currentProduct.ProductCode = item.ProductCode;
                        currentProduct.ProductName = item.ProductName;
                        currentProduct.StoreProductName = item.StoreProductName;
                        currentProduct.TaxCategoryValueId = item.TaxCategoryValueId;
                        currentProduct.LastUpdate = DateTime.Now;

                        if (item.CharValues != null) currentProduct = SetCharValueData(currentProduct, item.CharValues.ToList(), Repository.DbContext);
                        if (item.Suppliers != null) currentProduct = SetSupplierData(currentProduct, item.Suppliers.ToList(), Repository.DbContext);
                        if (item.ProductPictures != null) currentProduct = SetProductPictureData(currentProduct, item.ProductPictures.ToList(), Repository.DbContext);
                        currentProduct = SetMainSupplierData(currentProduct, item.MainSupplier, Repository.DbContext);
                        currentProduct = SetManucfatureData(currentProduct, item.Manufacture, Repository.DbContext);
                        currentProduct = SetProductGroupData(currentProduct, item.ProductGroup, Repository.DbContext);
                        currentProduct = SetMainProductGroupData(currentProduct, item.MainProductGroup, Repository.DbContext);
                        Repository.Update(currentProduct);
                    }
                    else
                    {
                        if (item.CharValues != null) item = SetCharValueData(item, item.CharValues.ToList(), Repository.DbContext);
                        if (item.Suppliers != null) item = SetSupplierData(item, item.Suppliers.ToList(), Repository.DbContext);
                        item = SetMainSupplierData(item, item.Suppliers.FirstOrDefault(), Repository.DbContext);
                        item = SetManucfatureData(item, item.Manufacture, Repository.DbContext);
                        item = SetProductGroupData(item, item.ProductGroup, Repository.DbContext);
                        item = SetMainProductGroupData(item, item.MainProductGroup, Repository.DbContext);
                        if (item.ProductPictures != null) item = SetProductPictureData(item, item.ProductPictures.ToList(), Repository.DbContext);
                        item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;

                        Repository.Add(item);
                    }
                });
                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Info("PublishAsync ", ex);
                throw ex;
            }
            log.Info("PublishAsync Finish" + ProductViewModels.Count);
        }

        public async Task Delete(List<ProductViewModel> ProductViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var products = Proxy.ReverseConvert(ProductViewModels);

                products.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }

        public Product SetProductGroupData(Product data, ProductGroup productGroup, AnatoliDbContext context)
        {
            if (productGroup == null) return data;
            var result = ProductGroupRepository.GetQuery().Where(p => p.Id == productGroup.Id).FirstOrDefault();
            if (result != null)
            {
                data.ProductGroup = result;
                data.ProductGroupId = result.Id;
            }
            else
            {
                data.ProductGroupId = null;
                data.ProductGroup = null;
            }
            return data;
        }

        public Product SetMainProductGroupData(Product data, MainProductGroup productGroup, AnatoliDbContext context)
        {
            if (productGroup == null) return data;
            var result = MainProductGroupRepository.GetQuery().Where(p => p.Id == productGroup.Id).FirstOrDefault();
            if (result != null)
            {
                data.MainProductGroup = result;
                data.MainProductGroupId = result.Id;
            }
            else
            {
                data.MainProductGroupId = null;
                data.MainProductGroup = null;
            }
            return data;
        }
        public Product SetProductPictureData(Product data, List<ProductPicture> productPictures, AnatoliDbContext context)
        {
            if (productPictures == null) return data;
            Repository.DbContext.Database.ExecuteSqlCommand("delete from ProductPictures where ProductId='" + data.Id +"'");
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
            var result = ManufactureRepository.GetQuery().Where(p => p.Id == manufacture.Id).FirstOrDefault();
            if (result != null)
            {
                data.Manufacture = result;
                data.ManufactureId = result.Id;
            }
            else
            {
                data.ManufactureId = null;
                data.Manufacture = null;
            }
            return data;
        }

        public Product SetMainSupplierData(Product data, Supplier suppliers, AnatoliDbContext context)
        {
            if (suppliers == null) return data;
            var supplier = SupplierRepository.GetQuery().Where(p => p.Id == suppliers.Id).FirstOrDefault();
            if (supplier != null)
            {
                data.MainSupplier = supplier;
                data.MainSuppliereId = supplier.Id;
            }
            else
            {
                data.MainSupplier = null;
                data.MainSuppliereId = null;
            }
            return data;
        }

        public Product SetSupplierData(Product data, List<Supplier> suppliers, AnatoliDbContext context)
        {
            if (suppliers == null) return data;
            //Repository.DbContext.Database.ExecuteSqlCommand("delete from ProductSupliers where ProductId='" + data.Id + "'");
            data.Suppliers.Clear();
            suppliers.ForEach(item =>
            {
                var supplier = SupplierRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                if (supplier != null)
                    data.Suppliers.Add(supplier);
            });
            return data;
        }

        public Product SetCharValueData(Product data, List<CharValue> charValues, AnatoliDbContext context)
        {
            if (charValues == null) return data;
            //Repository.DbContext.Database.ExecuteSqlCommand("delete from ProductChars where ProductId='" + data.Id + "'");
            data.CharValues.Clear();
            charValues.ForEach(item =>
            {
                var charType = CharValueRepository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Id == item.Id).FirstOrDefault();
                if (charType != null)
                    data.CharValues.Add(charType);
            });
            return data;
        }
        #endregion
    }
}
