using Anatoli.Common.DataAccess.Repositories;
using Anatoli.DataAccess.Interfaces.PersonnelAcitvity;
using Anatoli.DataAccess.Models.PersonnelAcitvity;

namespace Anatoli.DataAccess.Repositories.PersonnelAcitvity
{
    public class PersonnelDailyActivityEventRepository : AnatoliRepository<PersonnelDailyActivityEvent>, IPersonnelDailyActivityEventRepository
    {
        #region Ctors
        public PersonnelDailyActivityEventRepository() : this(new AnatoliDbContext()) { }
        public PersonnelDailyActivityEventRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
