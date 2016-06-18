using System;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.StockModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class ProductTypeDomain : BusinessDomainV2<ProductType, ProductTypeViewModel, ProductTypeRepository, IProductTypeRepository>, IBusinessDomainV2<ProductType, ProductTypeViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
         public ProductTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
         public ProductTypeDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
         protected override void AddDataToRepository(ProductType currentData, ProductType item)
         {
             if (currentData != null)
             {
                 if (currentData.ProductTypeName != item.ProductTypeName)
                 {
                     currentData.ProductTypeName = item.ProductTypeName;
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
