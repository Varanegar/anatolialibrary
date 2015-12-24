using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.CustomerModels
{
    public class CustomerViewModel : BaseViewModel
    {
        public List<BasketViewModel> Baskets { get; set; }
    }
}
