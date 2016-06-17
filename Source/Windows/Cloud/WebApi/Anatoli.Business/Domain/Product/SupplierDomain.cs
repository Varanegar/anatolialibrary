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
    public class SupplierDomain : BusinessDomainV2<Supplier, SupplierViewModel, SupplierRepository, ISupplierRepository>, IBusinessDomainV2<Supplier, SupplierViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
         public SupplierDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
         public SupplierDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(Supplier currentSupplier, Supplier item)
        {
            if (currentSupplier != null)
            {
                if (currentSupplier.SupplierName != item.SupplierName)
                {
                    currentSupplier.SupplierName = item.SupplierName;
                    currentSupplier.LastUpdate = DateTime.Now;
                    MainRepository.Update(currentSupplier);
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
