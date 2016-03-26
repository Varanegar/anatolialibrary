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
            this.HasRequired<ApplicationOwner>(p => p.ApplicationOwner).WithRequiredDependent().WillCascadeOnDelete(false);
            this.HasRequired<DataOwner>(p => p.DataOwner).WithRequiredDependent().WillCascadeOnDelete(false);
            this.HasRequired<DataOwnerCenter>(p => p.DataOwnerCenter).WithRequiredDependent().WillCascadeOnDelete(false);
            this.HasRequired<User>(p => p.AddedBy).WithRequiredDependent().WillCascadeOnDelete(false);
            this.HasRequired<User>(p => p.LastModifiedBy).WithRequiredDependent().WillCascadeOnDelete(false);
        }
    }
}