﻿using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Warehouse
{
    public class StoreOnHandEntity : BaseEntity
    {
        public Guid StoreId { get; set; }
        public DateTime UpdateDate { get; set; }
        public Dictionary<Guid,decimal> OnHandQty { get; set; }

    }
}