using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Anatoli.DataAccess.Configs
{
    public class StockProductConfig : EntityTypeConfiguration<StockProduct>
    {
        public StockProductConfig()
        {

        }
    }
}