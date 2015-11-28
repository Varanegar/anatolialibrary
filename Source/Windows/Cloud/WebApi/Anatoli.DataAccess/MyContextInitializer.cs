using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace Anatoli.DataAccess
{
    public class MyContextInitializer : CreateDatabaseIfNotExists<AnatoliDbContext>
    {
        protected override void Seed(AnatoliDbContext context)
        {
           //bootstrap some data or configs in your database.
        }
    }

}
