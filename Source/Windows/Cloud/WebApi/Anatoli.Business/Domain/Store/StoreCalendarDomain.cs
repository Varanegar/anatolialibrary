using System;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.StoreModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class StoreCalendarDomain : BusinessDomainV2<StoreCalendar, StoreCalendarViewModel, StoreCalendarRepository, IStoreCalendarRepository>, IBusinessDomainV2<StoreCalendar, StoreCalendarViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public StoreCalendarDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StoreCalendarDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<ICollection<StoreCalendarViewModel>> GetCalendarByStoreId(string storeId)
        {
            Guid storeGuid = Guid.Parse(storeId);
            return await GetAllAsync(p => p.Id == storeGuid);
        }

        protected override void AddDataToRepository(StoreCalendar currentStoreCalendars, StoreCalendar item)
        {
            if (currentStoreCalendars != null)
            {
                if (currentStoreCalendars.Date != item.Date ||
                    currentStoreCalendars.PDate != item.PDate ||
                    currentStoreCalendars.ToTime != item.ToTime ||
                    currentStoreCalendars.FromTime != item.FromTime ||
                    currentStoreCalendars.CalendarTypeValueId != item.CalendarTypeValueId ||
                    currentStoreCalendars.Description != item.Description)
                {
                    currentStoreCalendars.Date = item.Date;
                    currentStoreCalendars.PDate = item.PDate;
                    currentStoreCalendars.ToTime = item.ToTime;
                    currentStoreCalendars.FromTime = item.FromTime;
                    currentStoreCalendars.CalendarTypeValueId = item.CalendarTypeValueId;
                    currentStoreCalendars.Description = item.Description;
                    currentStoreCalendars.LastUpdate = DateTime.Now;
                    MainRepository.Update(item);
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
