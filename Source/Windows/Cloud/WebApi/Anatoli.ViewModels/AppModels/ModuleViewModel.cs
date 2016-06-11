using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.AppModels
{
    public class ModuleViewModel
    {
        public Guid Id { get; set; }

        public Guid AppId { get; set; }

        public string AppName { get; set; }

        public string ModuleName { get; set; }
    }
}
