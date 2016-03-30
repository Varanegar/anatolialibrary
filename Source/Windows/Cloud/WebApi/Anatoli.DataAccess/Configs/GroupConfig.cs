using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class GroupConfig : EntityTypeConfiguration<Group>
    {
        public GroupConfig()
        {
            this.HasMany<Principal>(s => s.Principals)
                .WithMany(c => c.Groups)
                .Map(cs =>
                {
                    cs.MapLeftKey("GroupId");
                    cs.MapRightKey("PrincipalID");
                    cs.ToTable("PrincipalGroups");
                });
        }
    }
}
