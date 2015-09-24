using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Product
{
    public class ProductCharGroupEntity : BaseEntity
    {
        public int GroupCode { get; set; }
        public string GroupName { get; set; }
        public List<int> GroupTypeList { get; set; }
    }
}
