using System;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using Anatoli.ViewModels.Product;
using System.Collections.Generic;
using Anatoli.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anatoli.Business;

namespace AnatoliUnitTests
{
    [TestClass]
    public class ProductRepositoryUnitTest
    {
        [TestMethod]
        public void AddingProductTestMethod()
        {

            Assert.IsTrue(AddProducts().Result);
        }

        private async Task<bool> AddProducts()
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var productDomain = new ProductDomain(owner);
            var group = new ProductGroupViewModel { GroupName = "g1", ID = 1, PrivateLabelKey = owner, UniqueId = Guid.NewGuid() };
            var suppliers = new List<SupplierViewModel> { new SupplierViewModel { ID = 1, Name = "sup1", PrivateLabelKey = owner, UniqueId = Guid.NewGuid() } };
            var manufacture = new ManufactureViewModel { ID = 1, Name = "man1", PrivateLabelKey = owner, UniqueId = Guid.NewGuid() };

            var models = new List<ProductViewModel>();
            for (int i = 0; i < 10; i++)
            {
                models.Add(new ProductViewModel
                {
                    ID = i + 1,
                    UniqueId = Guid.NewGuid(),
                    ProductName = "p" + i + 1,
                    PrivateLabelKey = owner,
                    ProductGroup = group,
                    Suppliers = suppliers,
                    Manufacture = manufacture,
                    PackUnitId=Guid.NewGuid(),
                    ProductTypeId=Guid.NewGuid(),
                    TaxCategoryId=Guid.NewGuid()                    
                });
            }

            await productDomain.Publish(models);

            return true;
        }
    }
}
