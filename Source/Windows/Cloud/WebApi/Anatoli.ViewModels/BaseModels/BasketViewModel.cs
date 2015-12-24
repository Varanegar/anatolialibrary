using Anatoli.ViewModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.BaseModels
{
    public class BasketViewModel : BaseViewModel
    {
        public Guid BasketTypeValueId { get; set; }
        public string BasketName { get; set; }
        public List<BasketItemViewModel> BasketItems { get; set; }

        public CustomerViewModel Customer { get; set; }
    }
}
