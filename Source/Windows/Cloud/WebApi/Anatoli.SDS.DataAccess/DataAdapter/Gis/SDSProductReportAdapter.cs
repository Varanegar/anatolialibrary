﻿using System.Collections.Generic;
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
                            "@SaleOfficeList ='" + (filter.SaleOffice == "" || filter.SaleOffice == "null" ? "-1" : filter.SaleOffice) + "'," +
                            "@SupervisorList ='" + (filter.Header == "" || filter.Header == "null" ? "-1" : filter.Header) + "'," +
                            "@DealerList = '"+(filter.Seller == "" || filter.Seller == "null" ? "-1" : filter.Seller)+"',"+
                            "@CustCtgrList ='" + (filter.CustomerClass == "" || filter.CustomerClass == "null" ? "-1" : filter.CustomerClass) + "'," +
                            "@CustActList = '" + (filter.CustomerActivity == "" || filter.CustomerActivity == "null" ? "-1" : filter.CustomerActivity) + "'," +
                            "@CustLevelList = '" + (filter.CustomerDegree == "" || filter.CustomerDegree == "null" ? "-1" : filter.CustomerDegree) + "'," +
                            "@CountNotVisit = '"+ filter.DayCount.ToString() +"',"+
                            "@GoodsGroupList = '" + (filter.GoodGroup == "" || filter.GoodGroup == "null" ? "-1" : filter.GoodGroup) + "'," +
                            "@SubTypeList = '" + (filter.DynamicGroup == "" || filter.DynamicGroup == "null" ? "-1" : filter.DynamicGroup).ToString() + "'," +
                            "@BrandList = '-1' ,"+
                            "@GoodsList = '" + (filter.Good == "" || filter.Good == "null" ? "-1" : filter.Good) + "'," +

                            "@UnSoldGoodsGroupList = '" + (filter.UnSoldGoodGroup == "" || filter.UnSoldGoodGroup == "null" ? "-1" : filter.UnSoldGoodGroup) + "'," +
                            "@UnSoldGoodsList = '" + (filter.UnSoldGood == "" || filter.UnSoldGood == "null" ? "-1" : filter.UnSoldGood) + "'," +

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
