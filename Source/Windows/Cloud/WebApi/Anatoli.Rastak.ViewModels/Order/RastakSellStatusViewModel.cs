using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.Rastak.ViewModels.Order
{
    public class RastakSellStatusViewModel : RastakBaseViewModel
    {
        public int SellId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Comment { get; set; }
        public Guid StatusTypeId { get; set; }
    }
}
