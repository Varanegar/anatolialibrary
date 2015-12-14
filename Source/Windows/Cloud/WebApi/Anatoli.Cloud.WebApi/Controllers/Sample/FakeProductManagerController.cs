using System;
using System.Linq;
using System.Web.Http;
using Anatoli.Business;
using System.Threading.Tasks;
using Anatoli.ViewModels.Product;
using System.Collections.Generic;

namespace Anatoli.Cloud.WebApi.Controllers.Sample
{
    [RoutePrefix("api/v0/sample/FakeProductManager")]
    public class FakeProductManagerController : ApiController
    {
        [Route("AddProduct")]
        [HttpGet]
        public async Task<bool> AddProduct()
        {
            return await AddProducts();
        }

        private async Task<bool> AddProducts()
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var productDomain = new ProductDomain(owner);
            var group = new ProductGroupViewModel { GroupName = "g1", ID = 1, PrivateLabelKey = owner, UniqueId = Guid.NewGuid() };
            var suppliers = new List<SupplierViewModel> { new SupplierViewModel { ID = 1, Name = "sup1", PrivateLabelKey = owner, UniqueId = Guid.NewGuid() } };
            var manufacture = new ManufactureViewModel { ID = 1, Name = "man1", PrivateLabelKey = owner, UniqueId = Guid.NewGuid() };

            var models = new List<ProductViewModel>();

            models.Add(new ProductViewModel
            {
                ID = 1,
                UniqueId = Guid.NewGuid(),
                ProductName = "p" + 1,
                PrivateLabelKey = owner,
                ProductGroup = group,
                Suppliers = suppliers,
                Manufacture = manufacture,
                PackUnitId = Guid.NewGuid(),
                ProductTypeId = Guid.NewGuid(),
                TaxCategoryId = Guid.NewGuid()
            });

            await productDomain.PublishAsync(models);

            return true;
        }

        [Route("GetProducts")]
        [HttpGet]
        public async Task<List<ProductViewModel>> GetProducts()
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var productDomain = new ProductDomain(owner);

            return await productDomain.GetAll();
        }
    }
}
