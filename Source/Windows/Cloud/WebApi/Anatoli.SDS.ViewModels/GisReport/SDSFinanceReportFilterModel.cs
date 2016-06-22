namespace Anatoli.SDS.ViewModels.GisReport
{
    public class SDSFinanceReportFilterModel
    {
        public int Type { set; get; }
        public string FromDate { set; get; }
        public string ToDate { set; get; }
        public string SaleOffice { set; get; }
        public string Header { set; get; }
        public string Seller { set; get; }
        public string CustomerClass { set; get; }
        public string CustomerActivity { set; get; }
        public string CustomerDegree { set; get; }
        public string GoodGroup { set; get; }
        public string DynamicGroup { set; get; }
        public string Good { set; get; }
        public string CommercialName { set; get; }
        public int? DayCount { set; get; }
        public string UnSoldGoodGroup { set; get; }
        public string UnSoldGood { set; get; }
        
    }
}
