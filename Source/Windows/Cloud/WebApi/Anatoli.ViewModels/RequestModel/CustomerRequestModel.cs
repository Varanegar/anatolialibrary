using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.CustomerModels;

namespace Anatoli.ViewModels
{
    public class CustomerRequestModel : BaseRequestModel
    {
        public Guid regionId { get; set; }
        public Guid customerId { get; set; }
        public Guid basketId { get; set; }
        public CustomerShipAddressViewModel customerShipAddressData { get; set; }
        public List<CustomerGroupViewModel> customerGroupData { get; set; }

        public CustomerViewModel customerData { get; set; }
        public List<BasketItemViewModel> basketItemData { get; set; }
        public List<BasketViewModel> basketData { get; set; }
        public List<CustomerViewModel> customerListData { get; set; }

    }
}
