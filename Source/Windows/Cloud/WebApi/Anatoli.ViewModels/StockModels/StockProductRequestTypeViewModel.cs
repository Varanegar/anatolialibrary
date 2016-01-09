using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestTypeViewModel : BaseViewModel
    {
        public static readonly Guid PeriodicalReqeust = Guid.Parse("1462A36B-9AB0-41AB-88F1-AAD152A7E425");
        public static readonly Guid ManualReqeust = Guid.Parse("252B2E6A-AC0E-49B7-9BE7-F559A2BAC847");
        public static readonly Guid UrgentReqeust = Guid.Parse("28BC2DEE-3839-48E7-B50B-D8B8D0CCA191");
        public static readonly Guid TestReqeust = Guid.Parse("47CF63BD-C297-441F-810E-0D685333CC39");

        public string StockProductRequestTypeName { get; set; }
    }
}
