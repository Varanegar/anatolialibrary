using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class CharTypeConfig : EntityTypeConfiguration<CharType>
    {
        public CharTypeConfig()
        {
            this.HasMany<CharGroup>(s => s.CharGroups)
                .WithMany(c => c.CharTypes)
                .Map(cs =>
                {
                    cs.MapLeftKey("CharTypeId");
                    cs.MapRightKey("CharGroupID");
                    cs.ToTable("CharGroupTypes");
                });

            this.HasMany<CharValue>(pc => pc.CharValues)
                .WithRequired(p => p.CharType);
        }
    }
}