using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security.Cryptography;
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
        public async Task<List<PersonnelDailyActivityPoint>> LoadPersonelsPath(string date, List<Guid> personIds, string fromTime, string toTime )
        {
            var fromH = Int16.Parse(fromTime.Substring(0, fromTime.IndexOf(":", System.StringComparison.Ordinal)));
            var fromM = Int16.Parse(fromTime.Substring(fromTime.IndexOf(":", System.StringComparison.Ordinal)+1));

            var toH = Int16.Parse(toTime.Substring(0, toTime.IndexOf(":", System.StringComparison.Ordinal)));
            var toM = Int16.Parse(toTime.Substring(toTime.IndexOf(":", System.StringComparison.Ordinal) + 1));

            var list = MainRepository.GetQuery().Where(x => 
                    (personIds.Contains(x.CompanyPersonnelId))                
                &&  (x.ActivityPDate == date)
                && ((x.ActivityDate.Hour > fromH) || ((x.ActivityDate.Hour == fromH)&&(x.ActivityDate.Minute >= fromM) ))
                && ((x.ActivityDate.Hour < toH) || ((x.ActivityDate.Hour == toH)&&(x.ActivityDate.Minute <= toM) ))
                ).OrderBy(x => x.ActivityDate);
            return await list.ToListAsync();              
        }


        public async Task<List<PersonnelDailyActivityPoint>> LoadPersonelsLastPoint(List<Guid> personIds)
        {

            var maxdte = MainRepository.GetQuery().GroupBy(x => x.CompanyPersonnelId)
                .Select(g => g.OrderByDescending(x => x.ActivityDate).FirstOrDefault().Id)
                .ToList();

            var query =  MainRepository.GetQuery().Where(r => 
                (personIds.Contains(r.CompanyPersonnelId)) &&
                (maxdte.Contains(r.Id) ));
          

            return await query.ToListAsync();
        }


        public async Task SavePersonelPoint(PersonnelDailyActivityPoint entity)
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
