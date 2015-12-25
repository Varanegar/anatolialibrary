using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anatoli.Business;
using Anatoli.Business.Domain;
using Anatoli.ViewModels.ProductModels;

namespace AnatoliUnitTests
{
    [TestClass]
    public class ProductRepositoryUnitTest
    {
        [TestMethod]
        public void AddingProductTestMethod()
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var productDomain = new ProductDomain(owner);
            var group = new ProductGroupViewModel { GroupName = "g1", ID = 1, PrivateLabelKey = owner, UniqueId = Guid.NewGuid() };
            var suppliers = new List<SupplierViewModel> { new SupplierViewModel { ID = 1, SupplierName = "sup1", PrivateLabelKey = owner, UniqueId = Guid.NewGuid() } };
            var manufacture = new ManufactureViewModel { ID = 1, ManufactureName = "man1", PrivateLabelKey = owner, UniqueId = Guid.NewGuid() };

            var models = new List<ProductViewModel>();
            
            models.Add(new ProductViewModel
            {
                ID = 1,
                UniqueId = Guid.NewGuid(),
                ProductName = "p" + 1,
                PrivateLabelKey = owner,
                //ProductGroup = group,
                Suppliers = suppliers,
                //Manufacture = manufacture,
                PackUnitId = Guid.NewGuid(),
                ProductTypeId = Guid.NewGuid(),
                TaxCategoryId = Guid.NewGuid()                
            });

            productDomain.PublishAsync(models);

            
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddingProductAsyncTestMethod()
        {
            Assert.IsTrue(AddProducts().Result);        
        }
        private async Task<bool> AddProducts()
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var productDomain = new ProductDomain(owner);
            var group = new ProductGroupViewModel { GroupName = "g1", ID = 1, PrivateLabelKey = owner, UniqueId = Guid.NewGuid() };
            var suppliers = new List<SupplierViewModel> { new SupplierViewModel { ID = 1, SupplierName = "sup1", PrivateLabelKey = owner, UniqueId = Guid.NewGuid() } };
            var manufacture = new ManufactureViewModel { ID = 1, ManufactureName = "man1", PrivateLabelKey = owner, UniqueId = Guid.NewGuid() };

            var models = new List<ProductViewModel>();
           
            models.Add(new ProductViewModel
            {
                ID = 1,
                UniqueId = Guid.NewGuid(),
                ProductName = "p" + 1,
                PrivateLabelKey = owner,
                //ProductGroup = group,
                Suppliers = suppliers,
                //Manufacture = manufacture,
                PackUnitId = Guid.NewGuid(),
                ProductTypeId = Guid.NewGuid(),
                TaxCategoryId = Guid.NewGuid()
            });
           
            await productDomain.PublishAsync(models);

            return true;
        }
    }
}
