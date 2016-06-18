using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class FiscalYearRepository : AnatoliRepository<FiscalYear>, IFiscalYearRepository
    {
        #region Ctors
        public FiscalYearRepository() : this(new AnatoliDbContext()) { }
        public FiscalYearRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
