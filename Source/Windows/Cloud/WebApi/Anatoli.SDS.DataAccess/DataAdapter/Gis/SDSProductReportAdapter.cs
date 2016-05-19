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
                            "@SaleOfficeList ='"+  IsNull(filter.SaleOffice, "-1").ToString() +"',"+
                            "@SupervisorList ='"+ IsNull(filter.Header, "-1")+"',"+
                            "@DealerList = '"+IsNull((filter.Seller == "" ? "-1" : filter.Seller), "-1")+"',"+
                            "@CustCtgrList ='"+ IsNull(filter.CustomerClass, "-1").ToString() +"',"+
                            "@CustActList = '"+IsNull(filter.CustomerActivity, "-1").ToString() +"',"+
                            "@CustLevelList = '"+IsNull(filter.CustomerDegree, "-1").ToString() +"',"+
                            "@CountNotVisit = '"+IsNull(filter.DayCount, "-1").ToString() +"',"+
                            "@GoodsGroupList = '" + IsNull((filter.GoodGroup == "" ? "-1" : filter.GoodGroup), "-1").ToString() + "'," +
                            "@SubTypeList = '"+IsNull(filter.DynamicGroup, "-1").ToString() +"',"+
                            "@BrandList = '-1' ,"+
                            "@GoodsList = '"+ IsNull((filter.Good == "" ? "-1" : filter.Good), "-1").ToString() +"',"+

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
