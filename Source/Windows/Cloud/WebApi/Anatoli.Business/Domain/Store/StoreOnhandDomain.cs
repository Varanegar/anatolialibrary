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
using EntityFramework.Extensions;
using Anatoli.Business.Helpers;

namespace Anatoli.Business.Domain
{
    public class StoreActiveOnhandDomain : BusinessDomainV2<StoreActiveOnhand, StoreActiveOnhandViewModel, StoreActiveOnhandRepository, IStoreActiveOnhandRepository>, IBusinessDomainV2<StoreActiveOnhand, StoreActiveOnhandViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StoreActiveOnhandDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StoreActiveOnhandDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<ICollection<StoreActiveOnhandViewModel>> GetAllByStoreId(Guid storeGuid)
        {
            return await GetAllAsync(p => p.StoreId == storeGuid);
        }
        public async Task<ICollection<StoreActiveOnhandViewModel>> GetAllByStoreIdChangedAfter(Guid storeGuid, DateTime selectedDate)
        {
            return await GetAllAsync(p => p.StoreId == storeGuid && p.LastUpdate >= selectedDate);
        }
        public List<StoreActiveOnhandViewModel> GetAllByStoreIdOnLine(Guid id)
        {
           return GetOnlineData(WebApiURIHelper.GetStoreOnHandLocalURI, "id=" + id);
        }

        protected override void AddDataToRepository(StoreActiveOnhand currentStoreAciveOnHand, StoreActiveOnhand item)
        {
            if (currentStoreAciveOnHand != null)
            {
                if (currentStoreAciveOnHand.Qty != item.Qty)
                {
                    currentStoreAciveOnHand.Qty = item.Qty;
                    currentStoreAciveOnHand.LastUpdate = DateTime.Now;
                    MainRepository.Update(currentStoreAciveOnHand);
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
