using System;
using System.Collections.Generic;
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


        public void UpdateReportCache(Guid guid, List<DMCProductReportCacheEntity> list)
        {


             var query = "INSERT INTO [dbo].[GisProductReportCache]" +
                        "([UniqueId]" +
                        ",[ClientId]" +
                        ",[Latitude]" +
                        ",[Longitude]" +
                        ",[Desc]" +
                        ",[OrderCount]" +
                        ",[SaleCount]" +
                        ",[RetSaleCount]" +
                        ",[SaleItemCount]" +
                        ",[RetSaleItemCount]" +
                        ",[SaleQty]" +
                        ",[SaleCarton]" +
                        ",[SaleAmount]" +
                        ",[RetSaleQty]" +
                        ",[RetSaleCarton]" +
                        ",[RetSaleAmount]" +
                        ",[SaleWeight]" +
                        ",[RetSaleWeight]" +
                        ",[SaleDiscount]" +
                        ",[RetSaleDiscount]" +
                        ",[SalePrizeCount]" +
                        ",[PrizeQty]" +
                        ",[PrizeCarton]" +
                        ",[IntId]" +
                        ")" +
                        "VALUES( NEWID(),  '" + guid.ToString() + "'" +
                        ",{0}" + //lat
                        ",{1}" + //long
                        ",'{2}'" + //[Desc]
                        ",{3}" + //OrderCount
                        ",{4}" + //SaleCount
                        ",{5}" + //RetSaleCount
                        ",{6}" + //SaleItemCount
                        ",{7}" + //RetSaleItemCount
                        ",{8}" + //SaleQty
                        ",{9}" + //SaleCarton
                        ",{10}" + //SaleAmount
                        ",{11}" + //RetSaleQty
                        ",{12}" + //RetSaleCarton
                        ",{13}" + //RetSaleAmount
                        ",{14}" + //SaleWeight
                        ",{15}" + //RetSaleWeight
                        ",{16}" + //SaleDiscount
                        ",{17}" + //RetSaleDiscount
                        ",{18}" + //SalePrizeCount
                        ",{19}" + //PrizeQty
                        ",{20}" + //PrizeCarton
                        ",{21}" + //customerId
                        ")";

            using (var context = GetDataContext())
            {
                context.Execute(string.Format("DELETE FROM GisProductReportCache WHERE ClientId = '{0}'", guid));

                foreach (var item in list)
                {
                    context.Execute(string.Format(query,
                        (item.Latitude ?? 0),
                        (item.Longitude ?? 0), //1
                        item.Desc, //2
                        item.OrderCount, //3
                        item.SaleCount, //4
                        item.RetSaleCount, //5
                        item.SaleItemCount, //6
                        item.RetSaleItemCount, //7
                        item.SaleQty, //8
                        item.SaleCarton, //
                        item.SaleAmount, //
                        item.RetSaleQty, //
                        item.RetSaleCarton, //
                        item.RetSaleAmount, //
                        item.SaleWeight, //
                        item.RetSaleWeight, //
                        item.SaleDiscount, //
                        item.RetSaleDiscount, //
                        item.SalePrizeCount, //
                        item.PrizeQty, //19
                        item.PrizeCarton, //20
                        item.IntId //20
                        ));
                }
                context.Commit();
            }
        }

        private object IsNull(object val, object def)
        {
            if (val != null) return val;
            return def;
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
