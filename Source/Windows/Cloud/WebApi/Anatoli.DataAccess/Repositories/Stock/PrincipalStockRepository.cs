using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class PrincipalStockRepository : AnatoliRepository<PrincipalStock>, IPrincipalStockRepository
    {
        #region Ctors
        public PrincipalStockRepository() : this(new AnatoliDbContext()) { }
        public PrincipalStockRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
