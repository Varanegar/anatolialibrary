using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.BaseModels
{
    public class ReorderCalcTypeViewModel : BaseViewModel
    {
        public static readonly Guid CalcProductOnly = Guid.Parse("7ECB9525-EA5F-487E-BBB6-971B9B22D7FF");
        public static readonly Guid CalcProductAndItsGroup = Guid.Parse("3341AFFC-6885-415E-8BEB-1EE0AA5B6405");
        public static readonly Guid CalcProductAndItsSupplier = Guid.Parse("780C2900-ED36-4D73-A898-54CDC326B5E7");

        public ReorderCalcTypeViewModel()
        {
            ReorderTypeName = "";
        }
        public string ReorderTypeName { get; set; }
    }
}
