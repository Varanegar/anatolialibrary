using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anatoli.ViewModels.LoyaltyModels
{
    public class LoyaltyCardViewModel : BaseViewModel
    {
        public string CardNo { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime GenerateDate { get; set; }
        public bool IsActive { get; set; }
        public bool AssignDate { get; set; }
        public Nullable<Guid> CancelReason { get; set; }
        
        public Guid LoyaltyCardSetId { get; set; }
        public string LoyaltyCardSetName { get; set; }

        public Nullable<Guid> LoyaltyCardBatchId { get; set; }
        //public string BatchGeneratePDate { get; set; }
        
        public Nullable<Guid> CustomerId { get; set; }
        public string CustomerName { get; set; }

    }
}
