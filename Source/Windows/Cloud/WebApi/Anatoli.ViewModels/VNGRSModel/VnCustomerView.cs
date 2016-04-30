using System;

namespace TrackingMap.Common.ViewModel
{
    public class VnCustomerView
    {
        public Guid Id { set; get; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }

        public bool HasLatLng { set; get; }
    }
}
