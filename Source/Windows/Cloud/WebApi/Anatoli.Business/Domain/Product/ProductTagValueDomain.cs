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
    public class ProductTagValueDomain : BusinessDomainV2<ProductTagValue, ProductTagValueViewModel, ProductTagValueRepository, IProductTagValueRepository>, IBusinessDomainV2<ProductTagValue, ProductTagValueViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
         public ProductTagValueDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
         public ProductTagValueDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
         protected override void AddDataToRepository(ProductTagValue currentData, ProductTagValue item)
         {
             if (currentData != null)
             {
                 if (currentData.ProductId != item.ProductId ||
                     currentData.ProductTagId != item.ProductTagId ||
                     currentData.FromDate != item.FromDate ||
                     currentData.ToDate != item.ToDate
                     )
                 {
                     currentData.ProductId = item.ProductId;
                     currentData.ProductTagId = item.ProductTagId;
                     currentData.FromDate = item.FromDate;
                     currentData.ToDate = item.ToDate;
                     currentData.LastUpdate = DateTime.Now;
                     MainRepository.Update(currentData);
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
