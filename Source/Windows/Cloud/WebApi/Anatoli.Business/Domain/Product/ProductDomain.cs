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
using System.Linq.Expressions;

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
                var model = await base.GetAllChangedAfterAsync(selectedDate);

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
        protected override Expression<Func<Product, ProductViewModel>> GetAllSelector()
        {
            return data => new ProductViewModel
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
                        ManufactureId = data.ManufactureId,
                        ManufactureName = data.Manufacture.ManufactureName,
                        ProductGroupId = data.ProductGroupId,
                        MainProductGroupId = data.MainProductGroupId,
                        MainSupplierId = data.MainSupplierId,
                        MainSupplierName = data.MainSupplier.SupplierName,
                        IsActiveInOrder = data.IsActiveInOrder,
                        ProductTypeInfo = data.ProductType != null ? new ProductTypeViewModel
                        {
                            ProductTypeName = data.ProductType.ProductTypeName,
                            UniqueId = data.ProductType.Id
                        } : null
                    };
        }
        protected override void AddDataToRepository(Product currentProduct, Product item)
        {
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

                //if (item.CharValues != null) currentProduct = SetCharValueData(currentProduct, item.CharValues.ToList(), DBContext);
                //if (item.Suppliers != null) currentProduct = SetSupplierData(currentProduct, item.Suppliers.ToList(), DBContext);
                MainRepository.Update(currentProduct);
            }
            else
            {
                item.ApplicationOwnerId = ApplicationOwnerKey; item.DataOwnerId = DataOwnerKey; item.DataOwnerCenterId = DataOwnerCenterKey;

                //if (item.CharValues != null) item = SetCharValueData(item, item.CharValues.ToList(), DBContext);
                //if (item.Suppliers != null) item = SetSupplierData(item, item.Suppliers.ToList(), DBContext);
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                if (item.ProductTypeId == null)
                    item.ProductType = null;
                MainRepository.Add(item);
            }
        }

        public async Task SetProductSupplierData(List<ProductSupplierViewModel> data)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;


                var products = GetDataListToCheckForExistsData();
                var suppliers = new SupplierDomain(ApplicationOwnerKey, DataOwnerKey, DataOwnerCenterKey, DBContext).GetDataListToCheckForExistsData();

                products.ForEach(item =>
                {
                    if (data.Count(p => p.ProductId == item.Id) > 0)
                        MainRepository.DbContext.Database.ExecuteSqlCommand("delete ProductSuppliers where productid='" + item.Id + "'");

                    data.FindAll(p => p.ProductId == item.Id).ForEach(pItem =>
                    {
                        MainRepository.DbContext.Database.ExecuteSqlCommand("insert into ProductSuppliers (productId, SupplierId) values ('" + item.Id + "', '" + pItem.SupplierId + "')");
                    });
                });
            }
            catch (Exception ex)
            {
                Logger.Error("PublishAsync ", ex);
                throw ex;
            }
            finally
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                Logger.Error("PublishAsync Finish" + data.Count);
            }

            await MainRepository.SaveChangesAsync();
        }

        public async Task SetProductCharValueData(List<ProductCharValueViewModel> data)
        {
            try
            {
                MainRepository.DbContext.Configuration.AutoDetectChangesEnabled = false;


                var products = GetDataListToCheckForExistsData();
                var charValues = new CharValueDomain(ApplicationOwnerKey, DataOwnerKey, DataOwnerCenterKey, DBContext).GetDataListToCheckForExistsData();

                products.ForEach(item =>
                {
                    if (data.Count(p => p.ProductId == item.Id) > 0)
                        MainRepository.DbContext.Database.ExecuteSqlCommand("delete ProductChars where productid='" + item.Id + "'");

                    data.FindAll(p => p.ProductId == item.Id).ForEach(pItem =>
                    {
                        MainRepository.DbContext.Database.ExecuteSqlCommand("insert into ProductChars (productId, CharValueId) values ('" + item.Id + "', '" + pItem.CharValueId + "')");
                    });

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
                Logger.Error("PublishAsync Finish" + data.Count);
            }

            await MainRepository.SaveChangesAsync();
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
        #endregion
    }
}
