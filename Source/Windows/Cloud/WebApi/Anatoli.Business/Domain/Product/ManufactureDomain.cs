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
    public class ManufactureDomain : BusinessDomainV2<Manufacture, ManufactureViewModel, ManufactureRepository, IManufactureRepository>, IBusinessDomainV2<Manufacture, ManufactureViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public ManufactureDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public ManufactureDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(Manufacture currentManufacture, Manufacture item)
        {
            if (currentManufacture != null)
            {
                if (currentManufacture.ManufactureName != item.ManufactureName)
                {
                    currentManufacture.ManufactureName = item.ManufactureName;
                    currentManufacture.LastUpdate = DateTime.Now;
                    MainRepository.Update(currentManufacture);
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
