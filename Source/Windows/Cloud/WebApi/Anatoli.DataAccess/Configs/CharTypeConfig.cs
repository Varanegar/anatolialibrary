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
            this.HasMany<CharGroupTypeInfo>(ct => ct.CharGroupTypeInfoes)
                .WithOptional(c => c.CharType);
        }
    }
}