using System;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class ProductGroupDomain : BusinessDomainV2<ProductGroup, ProductGroupViewModel, ProductGroupRepository, IProductGroupRepository>, IBusinessDomainV2<ProductGroup, ProductGroupViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public ProductGroupDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public ProductGroupDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(ProductGroup currentGroup, ProductGroup item)
        {
            if (currentGroup != null)
            {
                if (currentGroup.GroupName != item.GroupName ||
                        currentGroup.NLeft != item.NLeft ||
                        currentGroup.NRight != item.NRight ||
                        currentGroup.NLevel != item.NLevel ||
                        currentGroup.ProductGroup2Id != item.ProductGroup2Id)
                {

                    currentGroup.LastUpdate = DateTime.Now;
                    currentGroup.GroupName = item.GroupName;
                    currentGroup.NLeft = item.NLeft;
                    currentGroup.NRight = item.NRight;
                    currentGroup.NLevel = item.NLevel;
                    currentGroup.ProductGroup2Id = item.ProductGroup2Id;
                    MainRepository.Update(currentGroup);
                }
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
