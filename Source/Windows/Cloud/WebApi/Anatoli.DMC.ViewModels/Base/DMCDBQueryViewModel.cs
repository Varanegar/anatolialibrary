using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.DMC.ViewModels;

namespace Anatoli.DMC.ViewModels.BaseModels
{
    public class DMCDBQueryViewModel : DMCBaseViewModel
    {
        public Guid UniqueId { get; set; }
        public string QueryName { get; set; }
        public string QueryTSQL { get; set; }
    }
}
