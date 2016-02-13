using System;

namespace Anatoli.ViewModels.StockModels
{
    public class StockTypeViewModel : BaseViewModel
    {
        public static readonly Guid StoreStock = Guid.Parse("8a911bf9-dee1-449b-b407-a04a93e976c3");
        public static readonly Guid MainSCMStock = Guid.Parse("6ace8810-1261-4c47-98a0-f414c4d6f79a");
        public static readonly Guid BranchSCMStock = Guid.Parse("fe362433-401d-4b1f-b1fc-b75f0853bd44");
        public static readonly Guid BranchStock = Guid.Parse("92a9b97a-0e51-4ed3-b012-f1bde2e62f9b");
        public string StockTypeName { get; set; }


        public StockTypeViewModel()
        {
            StockTypeName = "";
        }
    }
}
