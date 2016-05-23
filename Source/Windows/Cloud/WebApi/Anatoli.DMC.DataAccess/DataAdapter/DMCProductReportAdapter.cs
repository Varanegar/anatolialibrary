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
using Thunderstruck;
using System.Data.SqlClient;

namespace Anatoli.DMC.DataAccess.DataAdapter
{
    public class DMCProductReportAdapter : DMCBaseAdapter
    {
        #region ctor

        private static DMCProductReportAdapter instance = null;

        public static DMCProductReportAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DMCProductReportAdapter();
                }
                return instance;
            }
        }

        private DMCProductReportAdapter()
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
       
        public void UpdateReportCache(Guid guid, List<DMCProductReportCacheEntity> list)
        {
            var con = GetConnectionString();
            using (var bulk = new SqlBulkCopy(con) { DestinationTableName = "GisProductReportCache" })
            {
                var saveData = list.ToList();
                var dd = GetDataTable<DMCProductReportCacheEntity>(saveData);
                bulk.WriteToServer(dd);

            }
        }

        public DMCProductReportForMapViewModel LoadGoodReport(Guid areaId, DMCProductReportFilterModel filter)
        {
            using (var context = GetDataContext(Transaction.No))
            {
                var views = context.First<DMCProductReportForMapViewModel>("exec GisLoadProductReport " +
                                                                           "@ClientId = '" + filter.ClientId + "'," +
                                                                           "@AreaId= '" + areaId + "'," +
                                                                           "@ShowOrderCount =" + filter.RequestCount +
                                                                           " ," +
                                                                           "@ShowSaleCount =" + filter.FactorCount +
                                                                           " ," +
                                                                           "@ShowRetSaleCount =" + filter.RejectCount +
                                                                           " ," +
                                                                           "@ShowSaleItemCount =" + filter.SaleItemCount +
                                                                           " ," +
                                                                           "@ShowRetSaleItemCount =" +
                                                                           filter.RejectItemCount + " ," +
                                                                           "@ShowSaleQty =" + filter.SaleQty + " ," +
                                                                           "@ShowSaleCarton =" + filter.SaleCarton +
                                                                           " ," +

                                                                           "@ShowRetSaleQty =" + filter.RejectQty + " ," +
                                                                           "@ShowRetSaleCarton =" + filter.RejectCarton +
                                                                           " ," +
                                                                           "@ShowSaleAmount =" + filter.SaleAmount +
                                                                           " ," +
                                                                           "@ShowRetSaleAmount =" + filter.RejectAmount +
                                                                           " ," +
                                                                           "@ShowSaleWeight =" + filter.SaleWeight +
                                                                           " ," +
                                                                           "@ShowRetSaleWeight =" + filter.RejectWeight +
                                                                           " ," +
                                                                           "@ShowSaleDiscount=" + filter.SaleDiscount +
                                                                           " ," +
                                                                           "@ShowRetSaleDiscount =" +
                                                                           filter.RejectDiscount + " ," +
                                                                           "@ShowPrizeCount =" + filter.BonusCount +
                                                                           " ," +
                                                                           "@ShowPrizeQty =" + filter.BonusQty + " ," +
                                                                           "@ShowPrizeCarton =" + filter.BonusCarton +
                                                                           " ," +
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
                var views = context.All<DMCPointViewModel>("exec GisLoadProductReportCustomer " +
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

        public bool RemoveProductReportCache(Guid guid)
        {
            using (var context = GetDataContext())
            {
                context.Execute("DELETE FROM  GisProductReportCache " +
                                "WHERE ClientId = '" + guid + "'");
                context.Commit();
                return true;
            }
        }

        public List<DMCPointViewModel> LoadProductValueReport(Guid id, DMCProductValueReportFilterModel filter)
        {

            var where = "";

            if (filter.FromRequestCount != null)
                where += "AND ( OrderCount >= " + filter.FromRequestCount + ")";
            if (filter.ToRequestCount != null)
                where += "AND ( OrderCount <= " + filter.ToRequestCount + ")";

            if (filter.FromFactorCount != null)
                where += "AND ( SaleCount >= " + filter.FromFactorCount + ")";
            if (filter.ToFactorCount != null)
                where += "AND ( SaleCount <= " + filter.ToFactorCount + ")";

            if (filter.FromRejectCount != null)
                where += "AND ( RetSaleCount >= " + filter.FromRejectCount + ")";
            if (filter.ToRejectCount != null)
                where += "AND ( RetSaleCount <= " + filter.ToRejectCount + ")";

            if (filter.FromSaleItemCount != null)
                where += "AND ( SaleItemCount >= " + filter.FromSaleItemCount + ")";
            if (filter.ToSaleItemCount != null)
                where += "AND ( SaleItemCount <= " + filter.ToSaleItemCount + ")";

            if (filter.FromRejectItemCount != null)
                where += "AND ( RetSaleItemCount >= " + filter.FromRejectItemCount + ")";
            if (filter.ToRejectItemCount != null)
                where += "AND ( RetSaleItemCount <= " + filter.ToRejectItemCount + ")";

            if (filter.FromSaleQty != null)
                where += "AND ( SaleQty >= " + filter.FromSaleQty + ")";
            if (filter.ToSaleQty != null)
                where += "AND ( SaleQty <= " + filter.ToSaleQty + ")";

            if (filter.FromSaleCarton != null)
                where += "AND ( SaleCarton >= " + filter.FromSaleCarton + ")";
            if (filter.ToSaleCarton != null)
                where += "AND ( SaleCarton <= " + filter.ToSaleCarton + ")";

            if (filter.FromSaleAmount != null)
                where += "AND ( SaleAmount >= " + filter.FromSaleAmount + ")";
            if (filter.ToSaleAmount != null)
                where += "AND ( SaleAmount <= " + filter.ToSaleAmount + ")";

            if (filter.FromRejectQty != null)
                where += "AND ( RetSaleQty >= " + filter.FromRejectQty + ")";
            if (filter.ToRejectQty != null)
                where += "AND ( RetSaleQty <= " + filter.ToRejectQty + ")";

            if (filter.FromRejectCarton != null)
                where += "AND ( RetSaleCarton >= " + filter.FromRejectCarton + ")";
            if (filter.ToRejectCarton != null)
                where += "AND ( RetSaleCarton <= " + filter.ToRejectCarton + ")";

            if (filter.FromRejectAmount != null)
                where += "AND ( RetSaleAmount >= " + filter.FromRejectAmount + ")";
            if (filter.ToRejectAmount != null)
                where += "AND ( RetSaleAmount <= " + filter.ToRejectAmount + ")";

            if (filter.FromSaleWeight != null)
                where += "AND ( SaleWeight >= " + filter.FromSaleWeight + ")";
            if (filter.ToSaleWeight != null)
                where += "AND ( SaleWeight <= " + filter.ToSaleWeight + ")";

            if (filter.FromRejectWeight != null)
                where += "AND ( RetSaleWeight >= " + filter.FromRejectWeight + ")";
            if (filter.ToRejectWeight != null)
                where += "AND ( RetSaleWeight <= " + filter.ToRejectWeight + ")";

            if (filter.FromSaleDiscount != null)
                where += "AND ( SaleDiscount >= " + filter.FromSaleDiscount + ")";
            if (filter.ToSaleDiscount != null)
                where += "AND ( SaleDiscount <= " + filter.ToSaleDiscount + ")";

            if (filter.FromRejectDiscount != null)
                where += "AND ( RetSaleDiscount >= " + filter.FromRejectDiscount + ")";
            if (filter.ToRejectDiscount != null)
                where += "AND ( RetSaleDiscount <= " + filter.ToRejectDiscount + ")";

            if (filter.FromBonusCount != null)
                where += "AND ( SalePrizeCount >= " + filter.FromBonusCount + ")";
            if (filter.ToBonusCount != null)
                where += "AND ( SalePrizeCount <= " + filter.ToBonusCount + ")";

            if (filter.FromBonusCarton != null)
                where += "AND ( PrizeCarton >= " + filter.FromBonusCarton + ")";
            if (filter.ToBonusCarton != null)
                where += "AND ( PrizeCarton <= " + filter.ToBonusCarton + ")";

            if (filter.FromBonusQty != null)
                where += "AND ( PrizeQty >= " + filter.FromBonusQty + ")";
            if (filter.ToBonusQty != null)
                where += "AND ( PrizeQty <= " + filter.ToBonusQty + ")";

            //if (filter.FromBonusAmount != null)
            //    where = "AND ( SaleCount >= " + filter.FromBonusAmount + ")";
            //if (filter.ToBonusAmount != null)
            //    where = "AND ( SaleCount <= " + filter.ToBonusAmount + ")";

            using (var context = GetDataContext(Transaction.No))
            {
                var list = context.All<DMCPointViewModel>("exec GisLoadGoodByValueReport " +
                                                          "@ClientId ='" + filter.ClientId + "'," +
                                                          "@AreaId = '" + id.ToString() + "', " +
                                                          "@HavingCondition = '" + where + "'"
                    ).ToList();

                return list;
            }
        }

        #endregion
    }
}
