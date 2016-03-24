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
using Anatoli.ViewModels.StoreModels;

namespace Anatoli.Business.Domain
{
    public class StoreActivePriceListDomain : BusinessDomainV2<StoreActivePriceList, StoreActivePriceListViewModel, StoreActivePriceListRepository, IStoreActivePriceListRepository>, IBusinessDomainV2<StoreActivePriceList, StoreActivePriceListViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StoreActivePriceListDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StoreActivePriceListDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<ICollection<StoreActivePriceListViewModel>> GetAllByStoreId(Guid storeGuid)
        {
            return await GetAllAsync(p => p.StoreId == storeGuid);
        }
        public async Task<ICollection<StoreActivePriceListViewModel>> GetAllByStoreIdChangedAfterAsync(Guid storeGuid, DateTime selectedDate)
        {
            return await GetAllAsync(p => p.StoreId == storeGuid && p.LastUpdate >= selectedDate);

        }

        protected override void AddDataToRepository(StoreActivePriceList currentStoreActivePriceList, StoreActivePriceList item)
        {
            if (currentStoreActivePriceList != null)
            {
                if (currentStoreActivePriceList.Price != item.Price)
                {
                    currentStoreActivePriceList.Price = item.Price;
                    currentStoreActivePriceList.LastUpdate = DateTime.Now;
                    MainRepository.Update(currentStoreActivePriceList);
                }
            }
            else
            {
                item.Id = Guid.NewGuid();
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }

        #endregion
    }
}
