using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Domain
{
    public class ItemImageDomain : BusinessDomainV2<ItemImage, ItemImageViewModel, ItemImageRepository, IItemImageRepository>, IBusinessDomainV2<ItemImage, ItemImageViewModel>
    {
        #region Properties
        #endregion

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
