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
    public class PersonnelDailyActivityPointDomain : BusinessDomainV2<PersonnelDailyActivityPoint, PersonnelDailyActivityPointViewModel, PersonnelDailyActivityPointRepository, IPersonnelDailyActivityPointRepository>, IBusinessDomainV2<PersonnelDailyActivityPoint, PersonnelDailyActivityPointViewModel>
    {
        #region Ctors
        public PersonnelDailyActivityPointDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public PersonnelDailyActivityPointDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        public async Task<List<PersonnelDailyActivityPoint>> LoadPersonelsPath(string date, List<Guid> personIds)
        {

            var list = await MainRepository.FindAllAsync(x => 
                    (personIds.Contains(x.CompanyPersonnelId))                
                &&  (x.ActivityPDate == date) 
                );
            return list.ToList();              
        }

        public async Task<List<PersonnelDailyActivityPoint>> LoadPersonelsLastPoint(List<Guid> personIds)
        {

            var list = await MainRepository.FindAllAsync(x => 
                    (personIds.Contains(x.CompanyPersonnelId))                
                );
            return list.ToList();              
        }


        
        #endregion

    }
}
