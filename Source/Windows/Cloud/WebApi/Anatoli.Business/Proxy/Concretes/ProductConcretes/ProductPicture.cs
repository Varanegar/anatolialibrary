using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.Concretes.ProductConcretes
{
    public class ProductPictureProxy : AnatoliProxy<ProductPicture, ProductPictureViewModel>, IAnatoliProxy<ProductPicture, ProductPictureViewModel>
    {
        public override ProductPictureViewModel Convert(ProductPicture data)
        {
            return new ProductPictureViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,
                //PictureTypeValueGuid = data.PictureTypeValueGuid
                IsDefault = data.IsDefault,
                ProductPictureName = data.ProductPictureName

            };
        }

        public override ProductPicture ReverseConvert(ProductPictureViewModel data)
        {
            return new ProductPicture
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PictureTypeValueGuid = Guid.Empty,
                IsDefault = data.IsDefault,
                ProductPictureName = data.ProductPictureName,

                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },
            };
        }
    }
}