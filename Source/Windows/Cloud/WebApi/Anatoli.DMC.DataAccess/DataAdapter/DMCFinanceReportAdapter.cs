using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.DataAccess.Helpers.Entity;
using Anatoli.DMC.ViewModels.Gis;
using Anatoli.DMC.ViewModels.Report;
using Anatoli.ViewModels.VnGisModels;
using Thunderstruck;
using System.Data.SqlClient;

namespace Anatoli.DMC.DataAccess.DataAdapter
{
    public class DMCFinanceReportAdapter : DMCBaseAdapter
    {
        #region ctor

        private static DMCFinanceReportAdapter instance = null;

        public static DMCFinanceReportAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DMCFinanceReportAdapter();
                }
                return instance;
            }
        }

        private DMCFinanceReportAdapter()
        {
        }

        #endregion

        #region method

        public static DataTable GetDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
       
        public void UpdateReportCache(Guid guid, List<DMCFinanceReportCacheEntity> list)
        {
            var con = GetConnectionString();
            using (var bulk = new SqlBulkCopy(con) { DestinationTableName = "GisFinanceReportCache" })
            {
                var saveData = list.ToList();
                var dd = GetDataTable<DMCFinanceReportCacheEntity>(saveData);
                bulk.WriteToServer(dd);

            }
        }

        public DMCFinanceReportForMapViewModel LoadFinanceReport(Guid areaId, DMCFinanceReportFilterModel filter)
        {
            using (var context = GetDataContext(Transaction.No))
            {

                var views = context.First<DMCFinanceReportForMapViewModel>("exec GisLoadFinanceReport " +
                                                                           "@ClientId = '" + filter.ClientId + "'," +
                                                                           "@AreaId= '" + areaId + "'," +
                                                                           "@ShowOpenFactorCount =" + filter.OpenFactorCount +" ," +
                                                                           "@ShowOpenFactorAmount =" + filter.OpenFactorAmount +" ," +
                                                                           "@ShowOpenFactorDay =" + filter.OpenFactorDay +" ," +
                                                                           "@ShowRejectChequeCount =" + filter.RejectChequeCount +" ," +
                                                                           "@ShowRejectChequeAmount =" + filter.RejectChequeAmount + " ," +
                                                                           "@ShowInprocessChequeCount =" + filter.InprocessChequeCount + " ," +
                                                                           "@ShowInprocessChequeAmount =" + filter.InprocessChequeAmount +" ," +

                                                                           "@ShowFirstCredit =" + filter.FirstCredit + " ," +
                                                                           "@ShowRemainedCredit =" + filter.RemainedCredit +" ," +
                                                                           "@ShowFirstDebitCredit =" + filter.FirstDebitCredit +" ," +
                                                                           "@ShowRemainedDebitCredit =" + filter.RemainedDebitCredit + " ," +
                                                                           "@HavingCondition='' ," +
                                                                           "@DefaultFieldIndex =" + filter.DefaultField
                    );

                return views;
            }
        }


        public List<DMCPointViewModel> LoadCustomer(Guid clientId, List<Guid> areaIds)
        {
            using (var context = GetDataContext(Transaction.No))
            {
                var views = context.All<DMCPointViewModel>("exec GisLoadFinanceReportCustomer " +
                                                           "@ClientId = '" + clientId + "'," +
                                                           "@Ids = '" + GuidListTostring(areaIds) + "'"

                    ).ToList();
                return views;
            }
        }

        public static string GuidListTostring(List<Guid> list)
        {
            var str = list.Aggregate("", (current, i) => current + (i.ToString() + ','));
            if (str.Length > 0)
                str = str.Remove(str.Length - 1);
            return str;
        }

        public bool RemoveFinanceReportCache(Guid guid)
        {
            using (var context = GetDataContext())
            {
                context.Execute("DELETE FROM  GisFinanceReportCache " +
                                "WHERE ClientId = '" + guid + "'");
                context.Commit();
                return true;
            }
        }

        public List<DMCPointViewModel> LoadFinanceValueReport(Guid? id, DMCFinanceValueReportFilterModel filter)
        {
            var custompoint = "";
            var firstpoints = "";


            if (id == null)
            {
                foreach (var point in filter.CustomPoint)
                {
                    if (custompoint == "")
                    {
                        firstpoints = point.Longitude + " " + point.Latitude;
                        custompoint = firstpoints;
                    }
                    else
                        custompoint += "," + point.Longitude + " " + point.Latitude;

                }
                if (custompoint != "")
                    custompoint += "," + firstpoints;

            }

            var where = "";

            if (filter.FromOpenFactorCount != null)
                where += "AND ( OpenFactorCount >= " + filter.FromOpenFactorCount + ")";
            if (filter.ToOpenFactorCount != null)
                where += "AND ( OpenFactorCount <= " + filter.ToOpenFactorCount + ")";

            if (filter.FromOpenFactorAmount != null)
                where += "AND ( OpenFactorAmount >= " + filter.FromOpenFactorAmount + ")";
            if (filter.ToOpenFactorAmount != null)
                where += "AND ( OpenFactorAmount <= " + filter.ToOpenFactorAmount + ")";

            if (filter.FromOpenFactorDay != null)
                where += "AND ( OpenFactorDay >= " + filter.FromOpenFactorDay + ")";
            if (filter.ToOpenFactorDay != null)
                where += "AND ( OpenFactorDay <= " + filter.ToOpenFactorDay + ")";

            if (filter.FromRejectChequeCount != null)
                where += "AND ( RejectChequeCount >= " + filter.FromRejectChequeCount + ")";
            if (filter.ToRejectChequeCount != null)
                where += "AND ( RejectChequeCount <= " + filter.ToRejectChequeCount + ")";

            if (filter.FromRejectChequeAmount != null)
                where += "AND ( RejectChequeAmount >= " + filter.FromRejectChequeAmount + ")";
            if (filter.ToRejectChequeAmount != null)
                where += "AND ( RejectChequeAmount <= " + filter.ToRejectChequeAmount + ")";

            if (filter.FromInprocessChequeCoun != null)
                where += "AND ( InprocessChequeCoun >= " + filter.FromInprocessChequeCoun + ")";
            if (filter.ToInprocessChequeCoun != null)
                where += "AND ( InprocessChequeCoun <= " + filter.ToInprocessChequeCoun + ")";

            if (filter.FromInprocessChequeAmount != null)
                where += "AND ( InprocessChequeAmount >= " + filter.FromInprocessChequeAmount + ")";
            if (filter.ToInprocessChequeAmount != null)
                where += "AND ( InprocessChequeAmount <= " + filter.ToInprocessChequeAmount + ")";

            if (filter.FromFirstCredit != null)
                where += "AND ( FirstCredit >= " + filter.FromFirstCredit + ")";
            if (filter.ToFirstCredit != null)
                where += "AND ( FirstCredit <= " + filter.ToFirstCredit + ")";

            if (filter.FromRemainedCredit != null)
                where += "AND ( RemainedCredit >= " + filter.FromRemainedCredit + ")";
            if (filter.ToRemainedCredit != null)
                where += "AND ( RemainedCredit <= " + filter.ToRemainedCredit + ")";

            if (filter.FromFirstDebitCredit != null)
                where += "AND ( FirstDebitCredit >= " + filter.FromFirstDebitCredit + ")";
            if (filter.ToFirstDebitCredit != null)
                where += "AND ( FirstDebitCredit <= " + filter.ToFirstDebitCredit + ")";

            if (filter.FromRemainedDebitCredit != null)
                where += "AND ( RemainedDebitCredit >= " + filter.FromRemainedDebitCredit + ")";
            if (filter.ToRemainedDebitCredit != null)
                where += "AND ( RemainedDebitCredit <= " + filter.ToRemainedDebitCredit + ")";

            using (var context = GetDataContext(Transaction.No))
            {
                var list = context.All<DMCPointViewModel>("exec GisLoadFinanceByValueReport " +
                                                          "@ClientId ='" + filter.ClientId + "'," +
                                                          "@AreaId = " + (id == null ? "null" : "'"+id+"'") + ", " +
                                                          "@HavingCondition = '" + where + "',"+
                                                          "@CustomPoints =  '"+ custompoint +"' "
                    ).ToList();

                return list;
            }
        }

        #endregion
    }
}
