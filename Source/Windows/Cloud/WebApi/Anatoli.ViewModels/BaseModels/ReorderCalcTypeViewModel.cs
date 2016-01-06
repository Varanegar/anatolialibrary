using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.BaseModels
{
    public class ReorderCalcTypeViewModel : BaseViewModel
    {
        public ReorderCalcTypeViewModel()
        {
            ReorderTypeName = "";
        }
        public string ReorderTypeName { get; set; }
    }
}
