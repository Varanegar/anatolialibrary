using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.AuthorizationModels
{
    public class PermissionSaveViewModel
    {
        public Guid ActionId { get; set; }

        public Guid ResourceId { get; set; }

        public string PermissionName { get; set; }

        public Guid PermissionId { get; set; }
    }
}
