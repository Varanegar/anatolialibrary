using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.BaseModels
{
    public class BasketItemViewModel : BaseViewModel
    {
        public int? Qty { get; set; }
        public string Comment { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
