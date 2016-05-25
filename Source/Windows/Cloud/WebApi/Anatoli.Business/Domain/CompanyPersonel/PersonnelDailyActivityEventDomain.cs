using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Interfaces.PersonnelAcitvity;
using Anatoli.DataAccess.Models.PersonnelAcitvity;
using Anatoli.DataAccess.Repositories.PersonnelAcitvity;
using Anatoli.ViewModels.CommonModels;
using Anatoli.ViewModels.PersonnelAcitvityModel;
using Anatoli.ViewModels.VnGisModels;

namespace Anatoli.Business.Domain.CompanyPersonel
{
    public class PersonnelDailyActivityEventDomain : BusinessDomainV2<PersonnelDailyActivityEvent, PersonnelDailyActivityEventViewModel, PersonnelDailyActivityEventRepository, IPersonnelDailyActivityEventRepository>, IBusinessDomainV2<PersonnelDailyActivityEvent, PersonnelDailyActivityEventViewModel>
    {
         #region Ctors
        public PersonnelDailyActivityEventDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public PersonnelDailyActivityEventDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods


        public async Task<List<PersonnelDailyActivityEvent>> LoadPersonelsEvents(string date,
            string fromTime, string toTime, List<Guid> personIds,
            bool order, bool lackOrder, bool lackVisit, bool stopWithoutActivity, bool stopWithoutCustomer)
        {
            var fromH = Int16.Parse(fromTime.Substring(0, fromTime.IndexOf(":", System.StringComparison.Ordinal)));
            var fromM = Int16.Parse(fromTime.Substring(fromTime.IndexOf(":", System.StringComparison.Ordinal) + 1));

            var toH = Int16.Parse(toTime.Substring(0, toTime.IndexOf(":", System.StringComparison.Ordinal)));
            var toM = Int16.Parse(toTime.Substring(toTime.IndexOf(":", System.StringComparison.Ordinal) + 1));

            var list = await MainRepository.FindAllAsync(x => (personIds.Contains(x.CompanyPersonnelId))
                && (x.ActivityPDate == date)
                && ((x.ActivityDate.Hour > fromH) || ((x.ActivityDate.Hour == fromH) && (x.ActivityDate.Minute >= fromM)))
                && ((x.ActivityDate.Hour < toH) || ((x.ActivityDate.Hour == toH) && (x.ActivityDate.Minute <= toM)))
                );
            return list.ToList();  
        }


        public async Task SavePersonelActivitie(PersonnelDailyActivityEvent entity)
        {
            try
            {
                await MainRepository.AddAsync(entity);

                await MainRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion

    }
}
