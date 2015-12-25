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
        public static Guid CheckOutBasketTypeId = Guid.Parse("F6CE03E2-8A2A-4996-8739-DA9C21EAD787");
        public static Guid FavoriteBasketTypeId = Guid.Parse("AE5DE00D-3391-49FE-985B-9DA7045CDB13");

        public BasketViewModel()
        {
        }
        public BasketViewModel(Guid basketTypeId)
        {
            BasketTypeValueId = basketTypeId;
        }

        public Guid BasketTypeValueId { get; set; }
        public string BasketName { get; set; }
        public List<BasketItemViewModel> BasketItems { get; set; }

        public CustomerViewModel Customer { get; set; }
    }
}
