using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.CustomerModels
{
    public class CustomerGroupViewModel : BaseViewModel
    {
        private string uniqueIdString;
        public string UniqueIdString
        {
            get
            {
                return this.uniqueIdString;
            }
            set
            {
                this.uniqueIdString = value;
                if (value != null)
                    this.UniqueId = Guid.Parse(value);
            }
        }

        public string ParentUniqueIdString { get; set; }
        public int ParentId { get; set; }
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public int? Priority { get; set; }
    }
}
