using Anatoli.DataAccess.Interfaces.PersonnelAcitvity;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Models.PersonnelAcitvity;

namespace Anatoli.DataAccess.Repositories.PersonnelAcitvity
{
    public class PersonnelDailyActivityPointRepository : AnatoliRepository<PersonnelDailyActivityPoint>, IPersonnelDailyActivityPointRepository
    {
        #region Ctors
        public PersonnelDailyActivityPointRepository() : this(new AnatoliDbContext()) { }
        public PersonnelDailyActivityPointRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
