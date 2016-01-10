using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestStatusViewModel : BaseViewModel
    {
        public static readonly Guid WaitForStoreManagerAcceptance = Guid.Parse("88B91155-5E56-4C48-BE2B-416C7EDA1713");
        public static readonly Guid WaitForSupervisorAcceptance = Guid.Parse("770803A2-3F46-48D1-98D2-2D656F6297DD");
        public static readonly Guid WaitForMainSupplyManagementAcceptance = Guid.Parse("C692FBF0-B133-4F01-B154-8441D62F65CF");
        public static readonly Guid WaitForSend = Guid.Parse("E0012511-EC7B-498C-8EE0-ECBF6A7EC63B");
        public static readonly Guid WaitForDeliver = Guid.Parse("9AE7E7BB-896E-4EFB-9279-D0EEFB830B3F");
        public string StockProductRequestStatusName { get; set; }
    }
}
