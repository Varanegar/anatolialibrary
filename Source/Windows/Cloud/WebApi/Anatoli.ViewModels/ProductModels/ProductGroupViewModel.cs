using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.ProductModels
{
    public class ProductGroupViewModel : BaseViewModel
    {
        public string UniqueIdString { get; set; }
        public int ParentId { get; set; }
        public Guid? ParentUniqueId { get; set; }
        public string ParentUniqueIdString { get; set; }
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        //public Guid CharGroupId { get; set; }
        public string CharGroupIdString { get; set; }

        //public bool IsRemoved { get; set; }
    }
}
