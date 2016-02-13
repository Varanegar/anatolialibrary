using Anatoli.DataAccess.Models;
using Anatoli.ViewModels.StockModels;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.Business.Proxy.Concretes.ProductConcretes
{
    public class ProductTypeProxy : AnatoliProxy<ProductType, ProductTypeViewModel>, IAnatoliProxy<ProductType, ProductTypeViewModel>
    {
        public override ProductTypeViewModel Convert(ProductType data)
        {
            return new ProductTypeViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,
                ProductTypeName = data.ProductTypeName
            };
        }

        public override ProductType ReverseConvert(ProductTypeViewModel data)
        {
            return new ProductType
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },
                ProductTypeName = data.ProductTypeName
            };
        }
    }
}