using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.AuthorizationModels
{
    public class PermissionDetailViewModel
    {
        public Guid ActionId { get; set; }

        public string ActionName { get; set; }

        public Guid PermissionId { get; set; }

        public string PermissionName { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; }

        public Guid ModuleId { get; set; }

        public string ModuleName { get; set; }

        public Guid ApplicationId { get; set; }

        public string ApplicationName { get; set; }
    }
}
