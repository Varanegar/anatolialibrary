using System.Collections.Generic;
using System.Linq;
using Anatoli.SDS.ViewModels.GisReport;
using Thunderstruck;

namespace Anatoli.SDS.DataAccess.DataAdapter.Gis
{
    public class SDSProductReportAdapter : SDSBaseAdapter
    {
        #region ctor
        private static SDSProductReportAdapter instance = null;
        public static SDSProductReportAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SDSProductReportAdapter();
                }
                return instance;
            }
        }
        private SDSProductReportAdapter() { }
        #endregion

        #region method

        public List<SDSProductReportViewModel> ReloadReportData(SDSProductReportFilterModel filter)
        {
            using (var context = GetDataContext(Transaction.No))
            {
                var views = context.All<SDSProductReportViewModel>("exec Usp_GIS_Load_getList " +

                            "@ReportType = 1 ,"+
                            "@DcList = '-1',"+

                            "@Startdate = '"+ filter.FromDate.Substring(2)+"',"+
                            "@EndDate = '"+ filter.ToDate.Substring(2)+"',"+
                            "@SaleOfficeList ='" + (filter.SaleOffice == "" ? "-1" : filter.SaleOffice) + "'," +
                            "@SupervisorList ='"+ (filter.Header == "" ? "-1" : filter.Header )+"',"+
                            "@DealerList = '"+(filter.Seller == "" ? "-1" : filter.Seller)+"',"+
                            "@CustCtgrList ='"+ (filter.CustomerClass == "" ?  "-1": filter.CustomerClass) +"',"+
                            "@CustActList = '"+(filter.CustomerActivity == "" ?  "-1": filter.CustomerActivity) +"',"+
                            "@CustLevelList = '"+(filter.CustomerDegree == "" ?  "-1" : filter.CustomerDegree) +"',"+
                            "@CountNotVisit = '"+ filter.DayCount.ToString() +"',"+
                            "@GoodsGroupList = '" + (filter.GoodGroup == "" ? "-1" : filter.GoodGroup) + "'," +
                                "@SubTypeList = '"+(filter.DynamicGroup == "" ? "-1" : filter.DynamicGroup).ToString() +"',"+
                            "@BrandList = '-1' ,"+
                            "@GoodsList = '"+ (filter.Good == "" ? "-1" : filter.Good) +"',"+

                            "@ShowOrderCount = true,"+
                            "@ShowSaleCount = true,"+
                            "@ShowRetSaleCount = true,"+
                            "@ShowSaleItemCount = true,"+
                            "@ShowRetSaleItemCount = true,"+
                            "@ShowSaleQty = true,"+
                            "@ShowSaleCarton = true,"+
                            "@ShowRetSaleQty = true,"+
                            "@ShowRetSaleCarton = true,"+
                            "@ShowSaleAmount = true,"+
                            "@ShowRetSaleAmount = true,"+
                            "@ShowSaleWeight = true,"+
                            "@ShowRetSaleWeight = true,"+
                            "@ShowSaleDiscount = true,"+
                            "@ShowRetSaleDiscount = true,"+
                            "@ShowPrizeCount = true,"+
                            "@ShowPrizeQty = true,"+
                            "@ShowPrizeCarton = true,"+
                            "@HavingCondition = '' "

                                ).ToList();

                return views;
            }
        }
        #endregion

        #region tools
        private object IsNull(object val, object def)
        {
            if (val != null) return val;
            return def;
        }


        #endregion
    }
}
