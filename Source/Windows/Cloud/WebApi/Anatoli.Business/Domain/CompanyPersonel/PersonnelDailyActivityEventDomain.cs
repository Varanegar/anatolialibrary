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


        public async Task<List<PersonnelDailyActivityEvent>> LoadPersonelsEvents(string date, List<Guid> personIds,
            bool order, bool lackOrder, bool lackVisit, bool stopWithoutActivity, bool stopWithoutCustomer)
        {
            var list = await MainRepository.FindAllAsync(x => (personIds.Contains(x.CompanyPersonnelId))
                && (x.ActivityPDate == date)

                
                );
            return list.ToList();  
        }


        #endregion

    }
}
