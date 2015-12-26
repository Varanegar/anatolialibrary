using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.ProductModels
{
    public class CharValueViewModel : BaseViewModel
    {
        public string CharValueText { get; set; }
        public decimal? CharValueFromAmount { get; set; }
        public decimal? CharValueToAmount { get; set; }
    }
}
