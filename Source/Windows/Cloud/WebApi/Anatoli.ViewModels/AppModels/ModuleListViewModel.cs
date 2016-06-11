using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.AppModels
{
    public class ModuleListViewModel
    {
        public ModuleListViewModel()
        {
            Modules = new List<ModuleViewModel>();
            Applications = new List<ApplicationViewModel>();
        }

        public List<ModuleViewModel> Modules { get; set; }

        public List<ApplicationViewModel> Applications { get; set; }
    }
}
