using System;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Repositories;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class ItemImageDomain : BusinessDomainV2<ItemImage, ItemImageViewModel, ItemImageRepository, IItemImageRepository>, IBusinessDomainV2<ItemImage, ItemImageViewModel>
    {
        #region Ctors
        public ItemImageDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public ItemImageDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(ItemImage currentItemImage, ItemImage item)
        {
            if (currentItemImage != null)
            {
                if(currentItemImage.TokenId != item.TokenId ||
                    currentItemImage.ImageName != item.ImageName ||
                    currentItemImage.IsDefault != item.IsDefault ||
                    currentItemImage.ImageType != item.ImageType
                    )
                    currentItemImage.LastUpdate = DateTime.Now;
                currentItemImage.TokenId = item.TokenId;
                currentItemImage.ImageName = item.ImageName;
                currentItemImage.IsDefault = item.IsDefault;
                currentItemImage.ImageType = item.ImageType;
                MainRepository.Update(currentItemImage);
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        #endregion
    }
}
