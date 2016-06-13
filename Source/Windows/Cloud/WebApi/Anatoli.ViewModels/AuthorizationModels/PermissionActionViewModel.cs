using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.AuthorizationModels
{
    public class PermissionActionViewModel
    {
        public PermissionActionViewModel()
        {
        }

        public Guid ActionId { get; set; }

        public string ActionName { get; set; }
    }
}
