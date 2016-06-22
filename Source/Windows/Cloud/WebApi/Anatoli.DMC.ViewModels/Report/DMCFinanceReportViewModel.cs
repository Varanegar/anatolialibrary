using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.ViewModels.Report
{
    public class DMCFinanceReportForMapViewModel
    {
        public Guid UniqueId { get; set; }
        public string JDesc { get; set; }
        public string Lable { get; set; }
        public string Title { get; set; }
        public bool IsLeaf { get; set; }

    }
}
