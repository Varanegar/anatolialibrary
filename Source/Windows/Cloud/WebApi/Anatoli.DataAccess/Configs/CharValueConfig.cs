using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class CharValueConfig : EntityTypeConfiguration<CharValue>
    {
        public CharValueConfig()
        {
            this.HasRequired<CharType>(ct => ct.CharType)
                .WithMany(c => c.CharValues);
        }
    }
}