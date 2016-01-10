using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public abstract class BaseModelConfig<T> : EntityTypeConfiguration<T> where T :BaseModel
    {
        public BaseModelConfig()
        {
            this.HasRequired<Principal>(p => p.PrivateLabelOwner).WithRequiredDependent().WillCascadeOnDelete(false);
            this.HasOptional<Principal>(p => p.AddedBy);
            this.HasOptional<Principal>(p => p.LastModifiedBy);
        }
    }
}