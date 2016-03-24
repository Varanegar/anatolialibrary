using System;
using System.Linq;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.Business.Proxy.ProductConcretes;
using System.Data.Entity;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Domain
{
    public class ProductDomain : BusinessDomainV2<Product, ProductViewModel, ProductRepository, IProductRepository>, IBusinessDomainV2<Product, ProductViewModel>
    {
        #region Properties
        public IRepository<Supplier> SupplierRepository { get; set; }
        public IRepository<CharValue> CharValueRepository { get; set; }

        #endregion

        #region Ctors
        public ProductDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, true)
        {

        }
        public ProductDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, bool fetchRemovedData)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public ProductDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            SupplierRepository = new SupplierRepository(dbc);
            CharValueRepository = new CharValueRepository(dbc);
        }
        #endregion

        #region Methods
        public async Task<List<ProductViewModel>> GetAllForLookup()
        {
            try
            {
                DBContext.Configuration.AutoDetectChangesEnabled = false;
                var model = await Task<List<ProductViewModel>>.Factory.StartNew(() =>
                {
                    return MainRepository.GetQuery()
                        .Where(
                                p => p.DataOwnerId == DataOwnerKey && p.IsRemoved == GetRemovedData
                            )
                    .Select(data => new ProductViewModel
                    {
                        ID = data.Number_ID,
                        UniqueId = data.Id,
                        ProductCode = data.ProductCode,
                        Barcode = data.Barcode,
                        StoreProductName = data.StoreProductName,
                        IsRemoved = data.IsRemoved
                    })
                    .AsNoTracking()
                    .ToList();
                });

                DBContext.Configuration.AutoDetectChangesEnabled = true;

                return model;
            }
            catch (Exception ex)
            {
                Logger.Error("GetAll ", ex);
                throw ex;
            }
        }

        public async Task<List<ProductViewModel>> Search(string term)
        {
            try
            {
                DBContext.Configuration.AutoDetectChangesEnabled = false;
                var model = await Task<List<ProductViewModel>>.Factory.StartNew(() =>
                {
                    return MainRepository.GetQuery()
                        .Where(
                                    p => p.DataOwnerId == DataOwnerKey && (p.StoreProductName.Contains(term) || p.ProductCode.Contains(term)) && p.IsRemoved == GetRemovedData
                            )
                    .Select(data => new ProductViewModel
                    {
                        ID = data.Number_ID,
                        UniqueId = data.Id,
                        ProductCode = data.ProductCode,
                        Barcode = data.Barcode,
                        StoreProductName = data.StoreProductName,
                        IsRemoved = data.IsRemoved
                    })
                    .AsNoTracking()
                    .ToList();
                });

                DBContext.Configuration.AutoDetectChangesEnabled = true;

                return model;
            }
            catch (Exception ex)
            {
                Logger.Error("Search ", ex);
                throw ex;
            }
        }

        public async Task<List<ProductViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            try
            {
                DBContext.Configuration.AutoDetectChangesEnabled = false;
                var model = await Task<List<ProductViewModel>>.Factory.StartNew(() =>
                {
                    return MainRepository.GetQuery()
                    .Where(p => p.DataOwnerId == DataOwnerKey && p.LastUpdate >= selectedDate && p.IsRemoved == GetRemovedData)
                    .Select(data => new ProductViewModel
                    {
                        ID = data.Number_ID,
                        UniqueId = data.Id,
                        ProductCode = data.ProductCode,
                        Desctription = data.Desctription,
                        PackVolume = data.PackVolume,
                        PackWeight = data.PackWeight,
                        Barcode = data.Barcode,
                        StoreProductName = data.StoreProductName,
                        ProductTypeId = data.ProductTypeId,
                        QtyPerPack = data.QtyPerPack,
                        IsRemoved = data.IsRemoved,
                        ManufactureIdString = data.ManufactureId.ToString(),
                        ManufactureName = data.Manufacture.ManufactureName,
                        ProductGroupIdString = data.ProductGroupId.ToString(),
                        MainProductGroupIdString = data.MainProductGroupId.ToString(),
                        MainSupplierId = data.MainSupplierId.ToString(),
                        MainSupplierName = data.MainSupplier.SupplierName,
                        IsActiveInOrder = data.IsActiveInOrder,
                        ProductTypeInfo = data.ProductType != null ? new ProductTypeViewModel
                        {
                            ProductTypeName = data.ProductType.ProductTypeName,
                            UniqueId = data.ProductType.Id
                        } : null
                    })
                    .AsNoTracking()
                    .ToList();
                });

                model.Where(p => p.ProductTypeInfo == null).ToList().ForEach(itm => itm.ProductTypeInfo = new ProductTypeViewModel());
                DBContext.Configuration.AutoDetectChangesEnabled = true;

                return model;
            }
            catch (Exception ex)
            {
                Logger.Error("GetAll ", ex);
                throw ex;
            }
        }

        public async Task<ICollection<Product>> PublishAsync(List<Product> products)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;


                var currentProductList = GetDataListToCheckForExistsData();

                products.ForEach(item =>
                {
                    var currentProduct = currentProductList.Find(p => p.Id == item.Id);

                    if (currentProduct != null)
                    {
                        currentProduct.ProductName = item.ProductName;
                        currentProduct.ProductGroupId = item.ProductGroupId;
                        currentProduct.ManufactureId = item.ManufactureId;
                        currentProduct.Desctription = item.Desctription;
                        currentProduct.QtyPerPack = item.QtyPerPack;
                        currentProduct.PackVolume = item.PackVolume;
                        currentProduct.PackWeight = item.PackWeight;
                        currentProduct.ProductCode = item.ProductCode;
                        currentProduct.Barcode = item.Barcode;
                        currentProduct.ProductName = item.ProductName;
                        currentProduct.StoreProductName = item.StoreProductName;
                        currentProduct.TaxCategoryValueId = item.TaxCategoryValueId;
                        currentProduct.MainProductGroupId = item.MainProductGroupId;
                        currentProduct.ManufactureId = item.ManufactureId;
                        currentProduct.MainSupplierId = item.MainSupplierId;
                        currentProduct.LastUpdate = DateTime.Now;

                        if (item.ProductType != null)
                            currentProduct.ProductTypeId = item.ProductTypeId;

                        if (item.CharValues != null) currentProduct = SetCharValueData(currentProduct, item.CharValues.ToList(), DBContext);
                        if (item.Suppliers != null) currentProduct = SetSupplierData(currentProduct, item.Suppliers.ToList(), DBContext);
                        MainRepository.Update(currentProduct);
                    }
                    else
                    {
                        item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;

                        if (item.CharValues != null) item = SetCharValueData(item, item.CharValues.ToList(), DBContext);
                        if (item.Suppliers != null) item = SetSupplierData(item, item.Suppliers.ToList(), DBContext);
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        if (item.ProductTypeId == null)
                            item.ProductType = null;
                        MainRepository.Add(item);
                    }

                });
                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error("PublishAsync ", ex);
                throw ex;
            }
            finally
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                Logger.Error("PublishAsync Finish" + products.Count);
            }
            return products;

        }

        public Product SetSupplierData(Product data, List<Supplier> suppliers, AnatoliDbContext context)
        {
            if (suppliers == null) return data;
            data.Suppliers.Clear();
            suppliers.ForEach(item =>
            {
                var supplier = SupplierRepository.GetQuery().Where(p => p.DataOwnerId == DataOwnerKey && p.Id == item.Id).FirstOrDefault();
                if (supplier != null)
                    data.Suppliers.Add(supplier);
            });
            return data;
        }

        public async Task ChangeProductTypes(List<ProductViewModel> data)
        {
            foreach (var item in data)
            {
                var product = await MainRepository.GetByIdAsync(item.UniqueId);

                if (item.ProductTypeInfo != null && item.ProductTypeInfo.UniqueId != Guid.Empty)
                    product.ProductTypeId = item.ProductTypeInfo.UniqueId;

                product.IsActiveInOrder = item.IsActiveInOrder;

                await MainRepository.SaveChangesAsync();
            }

        }

        public Product SetCharValueData(Product data, List<CharValue> charValues, AnatoliDbContext context)
        {
            if (charValues == null) return data;
            data.CharValues.Clear();
            charValues.ForEach(item =>
            {
                var charType = CharValueRepository.GetQuery().Where(p => p.DataOwnerId == DataOwnerKey && p.Id == item.Id).FirstOrDefault();
                if (charType != null)
                    data.CharValues.Add(charType);
            });
            return data;
        }

        #endregion
    }
}
