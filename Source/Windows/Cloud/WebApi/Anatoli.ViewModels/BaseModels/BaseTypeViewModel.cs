using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.BaseModels
{
    public class BaseTypeViewModel : BaseViewModel
    {
        public string BaseTypeDesc { get; set; }
        public List<BaseValueViewModel> BaseValues { get; set; }
    }
}
