using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.ViewModels.Area
{
    public class DMCVisitTemplatePathViewModel : DMCBaseViewModel
    {
        public Guid UniqueId { set; get; }

        public Guid? ParentUniqueId { set; get; }

        public string PathTitle { set; get; }

        public bool IsLeaf { set; get; }
    }
}
