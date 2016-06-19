using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Common.DataAccess.Repositories;

namespace Anatoli.DataAccess.Repositories
{
    public class ReorderCalcTypeRepository : AnatoliRepository<ReorderCalcType>, IReorderCalcTypeRepository
    {
        #region Ctors
        public ReorderCalcTypeRepository() : this(new AnatoliDbContext()) { }
        public ReorderCalcTypeRepository(AnatoliDbContext context)
            : base(context)
        {
        }
        #endregion
    }
}
