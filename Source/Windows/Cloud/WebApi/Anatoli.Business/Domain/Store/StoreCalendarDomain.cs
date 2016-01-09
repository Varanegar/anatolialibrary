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
    public class StoreCalendarDomain : BusinessDomain<StoreCalendarViewModel>, IBusinessDomain<StoreCalendar, StoreCalendarViewModel>
    {
        #region Properties
        public IAnatoliProxy<StoreCalendar, StoreCalendarViewModel> Proxy { get; set; }
        public IRepository<StoreCalendar> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        StoreCalendarDomain() { }
        public StoreCalendarDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StoreCalendarDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StoreCalendarRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<StoreCalendar, StoreCalendarViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StoreCalendarDomain(IRepository<StoreCalendar> repository, IPrincipalRepository principalRepository, IAnatoliProxy<StoreCalendar, StoreCalendarViewModel> proxy)
        {
            Proxy = proxy;
            Repository = repository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<StoreCalendarViewModel>> GetCalendarByStoreId(string storeId)
        {
            Guid storeGuid = Guid.Parse(storeId); 
            var storeCalendars = await Repository.FindAllAsync(p => p.Id == storeGuid);

            return Proxy.Convert(storeCalendars.ToList());
        }

        public async Task<List<StoreCalendarViewModel>> GetAll()
        {
            var storeCalendars = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(storeCalendars.ToList()); 
        }

        public async Task<List<StoreCalendarViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var storeCalendars = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(storeCalendars.ToList()); ;
        }

        public async Task<List<StoreCalendarViewModel>> PublishAsync(List<StoreCalendarViewModel> dataViewModels)
        {
            try
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var storeCalendars = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                storeCalendars.ForEach(item =>
                {
                    var currentStoreCalendars = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (currentStoreCalendars != null)
                    {
                        if (currentStoreCalendars.Date != item.Date ||
                            currentStoreCalendars.PDate != item.PDate ||
                            currentStoreCalendars.ToTime != item.ToTime ||
                            currentStoreCalendars.FromTime != item.FromTime ||
                            currentStoreCalendars.Description != item.Description)
                        {
                            currentStoreCalendars.Date = item.Date;
                            currentStoreCalendars.PDate = item.PDate;
                            currentStoreCalendars.ToTime = item.ToTime;
                            currentStoreCalendars.FromTime = item.FromTime;
                            currentStoreCalendars.Description = item.Description;
                            currentStoreCalendars.LastUpdate = DateTime.Now;
                            Repository.Update(item);
                        }
                    }
                    else
                    {
                        item.Id = Guid.NewGuid();
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        Repository.Add(item);
                    }
                });
                await Repository.SaveChangesAsync();
            }catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
            }
            finally
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                log.Info("PublishAsync Finish" + dataViewModels.Count);
            }
            return dataViewModels;
        }

        public async Task<List<StoreCalendarViewModel>> Delete(List<StoreCalendarViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var storeCalendars = Proxy.ReverseConvert(dataViewModels);

                storeCalendars.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.StoreCalendars.Remove(product);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }
        #endregion
    }
}
