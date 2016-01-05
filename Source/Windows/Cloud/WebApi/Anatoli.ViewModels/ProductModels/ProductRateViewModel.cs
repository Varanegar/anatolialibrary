using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.ProductModels
{
    public class ProductRateViewModel : BaseViewModel
    {
        public int ProductRateValue { get; set; }
        public Guid? RateBy { get; set; }
        public string RateByName { get; set; }
        public DateTime RateDate { get; set; }
        public TimeSpan RateTime { get; set; }
        public double Avg { get; set; }
        public Guid ProductGuid { get; set; }
    }
}
