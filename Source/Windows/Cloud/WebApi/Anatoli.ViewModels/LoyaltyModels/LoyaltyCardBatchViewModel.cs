using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anatoli.ViewModels;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyCardBatchViewModel : BaseViewModel
    {
        //public List<LoyaltyCardViewMoodel> LoyaltyCards { get; set; }
        public DateTime BatchGenerateDate { get; set; }
        public string BatchGeneratePDate { get; set; }
    }
}
