using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.AppModels
{
    public class ResourceViewModel
    {
        public Guid ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public Guid ModuleId { get; set; }

        public string ModuleName { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; }
    }
}
