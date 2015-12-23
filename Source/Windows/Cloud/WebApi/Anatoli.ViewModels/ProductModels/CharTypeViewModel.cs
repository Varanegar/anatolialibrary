using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.ProductModels
{
    public class CharTypeViewModel : BaseViewModel
    {
        public string CharTypeDesc { get; set; }
        public Guid DefaultCharValueID { get; set; }
        public List<CharValueViewModel> CharValues { get; set; }
    }
}
