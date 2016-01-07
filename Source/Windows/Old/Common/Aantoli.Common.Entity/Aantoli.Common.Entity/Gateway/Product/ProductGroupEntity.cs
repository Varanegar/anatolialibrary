using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Product
{
    public class ProductGroupEntity : BaseEntity
    {
        public int ParentId { get; set; }
        public Guid ParentUniqueId { get; set; }
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public Guid CharGroupId { get; set; }
    }
}
