using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels.StockModels
{
    public class StockProductRequestRuleTypeViewModel : BaseViewModel
    {
        public static readonly Guid BasedOnRerderLevelWithRelated = Guid.Parse("9a82849b-178f-4da1-85e7-700fee41a58a");
        public static readonly Guid BasedOnRerderLevelWithoutRelated = Guid.Parse("9f1f9ce4-4d5d-4885-9458-16eb24bf1b59");
        public static readonly Guid AddToRequest = Guid.Parse("4a840ae4-2bd5-406c-9dca-8e582b41f8de");

        public string StockProductRequestRuleTypeName { get; set; }
    }
    public class StockProductRequestRuleCalcTypeViewModel : BaseViewModel
    {
        public string StockProductRequestRuleCalcTypeName { get; set; }
    }
}
