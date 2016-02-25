using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.ProductModels
{
    public class ProductTagValueViewModel : BaseViewModel
    {
        public DateTime? FromDate { get; set; }
        public string FromPDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ToPDate { get; set; }
        public Guid ProductTagId { get; set; }
        public Guid ProductId { get; set; }
    }
}
