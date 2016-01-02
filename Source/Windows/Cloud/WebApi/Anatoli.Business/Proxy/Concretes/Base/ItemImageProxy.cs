using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class ItemImageProxy : AnatoliProxy<ItemImage, ItemImageViewModel>, IAnatoliProxy<ItemImage, ItemImageViewModel>
    {
        public override ItemImageViewModel Convert(ItemImage data)
        {
            return new ItemImageViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,
                ImageName = data.ImageName,
                BaseDataId = data.TokenId,
                ImageType = data.ImageType,

            };
        }

        public override ItemImage ReverseConvert(ItemImageViewModel data)
        {
            ItemImage tempItem = new ItemImage()
            {
                Number_ID = data.ID,
                Id = data.UniqueId,

                ImageName = data.ImageName,
                TokenId = data.BaseDataId,
                ImageType = data.ImageType,

                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },

            };


            return tempItem;
        }
    }
}