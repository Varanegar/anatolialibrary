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

namespace Anatoli.Business.Domain
{
    public class BrandDomain : BusinessDomainV2<Brand, BrandViewModel, BrandRepository, IBrandRepository>, IBusinessDomainV2<Brand, BrandViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public BrandDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public BrandDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(Brand currentBrand, Brand item)
        {
            if (currentBrand != null)
            {
                if (currentBrand.BrandName != item.BrandName)
                {
                    currentBrand.BrandName = item.BrandName;
                    currentBrand.LastUpdate = DateTime.Now;
                    MainRepository.Update(currentBrand);
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
