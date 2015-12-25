using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.BaseModels
{
    public class CityRegionViewModel : BaseViewModel
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
                this.UniqueId = Guid.Parse(value);
            }
        }
        public Guid ParentId { get; set; }
        public string GroupName { get; set; }
        public int NLeft { get; set; }
        public int NRight { get; set; }
        public int NLevel { get; set; }
        public int? Priority { get; set; }
    }
}
