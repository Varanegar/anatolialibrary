using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.ViewModels;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyCardSetViewModel : BaseViewModel
    {
        public string LoyaltyCardSetName { get; set; }
        public int Seed { get; set; }
        public long CurrentNo { get; set; }
        public string Initialtext { get; set; }
        public Guid GenerateNoType { get; set; }

        //public List<LoyaltyCardBatchViewModel> LoyaltyCardBatchs { get; set; }
        //public List<LoyaltyCardViewModel> LoyaltyCards { get; set; }
    }
}
