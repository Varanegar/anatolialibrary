using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.VnGisModels
{
    public class CustomerComboViewModel
    {
        public Guid UniqueId { set; get; }
        public string Title { get; set; }
        public bool HasLatLng { set; get; }
    }
}
