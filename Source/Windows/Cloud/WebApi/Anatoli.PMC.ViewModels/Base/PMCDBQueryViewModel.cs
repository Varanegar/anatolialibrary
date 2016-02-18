using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.PMC.ViewModels;

namespace Anatoli.PMC.ViewModels.BaseModels
{
    public class PMCDBQueryViewModel : PMCBaseViewModel
    {
        public Guid UniqueId { get; set; }
        public string QueryName { get; set; }
        public string QueryTSQL { get; set; }
    }
}
