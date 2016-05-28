using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.VnGisModels
{
    public class CustomerComboViewModel
    {
        public Guid uniqueId { set; get; }
        public string title { get; set; }
        public bool hasLatLng { set; get; }
    }
}
