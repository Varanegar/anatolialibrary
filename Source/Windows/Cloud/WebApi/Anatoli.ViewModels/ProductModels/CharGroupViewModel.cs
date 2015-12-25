using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.ProductModels
{
    public class CharGroupViewModel : BaseViewModel
    {
        public int CharGroupCode { get; set; }
        public string CharGroupName { get; set; }
        public List<Guid> CharTypeGuids { get; set; }
        public List<CharTypeViewModel> CharTypes { get; set; }

    }
}
