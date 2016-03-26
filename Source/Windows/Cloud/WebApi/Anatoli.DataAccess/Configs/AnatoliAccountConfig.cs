using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class AnatoliAccountConfig : EntityTypeConfiguration<AnatoliAccount>
    {
        public AnatoliAccountConfig()
        {
        }
    }
}
