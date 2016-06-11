using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.AppModels
{
    public class ApplicationItemViewModel
    {
        public ApplicationItemViewModel()
        {
            Modules = new List<ApplicationModuleItemViewModel>();
        }

        public Guid ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public List<ApplicationModuleItemViewModel> Modules { get; set; }
    }

    public class ApplicationModuleItemViewModel
    {
        public ApplicationModuleItemViewModel()
        {
            Resources = new List<ApplicationModuleResourceItemViewModel>();
        }

        public Guid ApplicationId { get; set; }

        public Guid ModuleId { get; set; }

        public string ModuleName { get; set; }

        public List<ApplicationModuleResourceItemViewModel> Resources { get; set; }
    }

    public class ApplicationModuleResourceItemViewModel
    {
        public ApplicationModuleResourceItemViewModel()
        {
        }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; }

        public Guid ModuleId { get; set; }
    }
}
