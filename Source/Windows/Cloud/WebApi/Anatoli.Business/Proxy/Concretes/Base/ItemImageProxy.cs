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
                ApplicationOwnerId = data.ApplicationOwnerId,
                ImageName = data.ImageName,
                BaseDataId = data.TokenId,
                IsDefault = data.IsDefault,
                ImageType = data.ImageType,
                IsRemoved = data.IsRemoved,

            };
        }

        public override ItemImage ReverseConvert(ItemImageViewModel data)
        {
            ItemImage tempItem = new ItemImage()
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                IsDefault = data.IsDefault,
                IsRemoved = data.IsRemoved,
                
                ImageName = data.ImageName,
                TokenId = data.BaseDataId,
                ImageType = data.ImageType,

                ApplicationOwnerId = data.ApplicationOwnerId,

            };


            return tempItem;
        }
    }
}