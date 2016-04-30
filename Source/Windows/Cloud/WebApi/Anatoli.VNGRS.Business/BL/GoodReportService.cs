using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingMap.Common.Enum;
using TrackingMap.Common.Tools;
using TrackingMap.Common.ViewModel;
using TrackingMap.Service.DBManagement;
using TrackingMap.Service.Entity;
using EntityFramework.BulkInsert.Extensions;
using Microsoft.Samples.EntityDataReader;
using TrackingMap.Service.ViewModel;

namespace TrackingMap.Service.BL
{
    public class GoodReportService
    {
        private readonly IDbContext _ctx;


        public GoodReportService(IDbContext ctx)
        {
            _ctx = ctx;
        }

        public void RemoveByClientId(Guid clientId)
        {
            _ctx.GetDatabase().ExecuteSqlCommand(string.Format("delete from GoodReportCache where ClientId = '{0}'", clientId));
        }

        public void UpdateReportCache(Guid clientId, List<GoodReportView> list)
        {
            LogService.InsertLog("start ", "UpdateReportCache", ELogLevel.DEBUG);
            RemoveByClientId(clientId);
            var con = DbUtility.GetConnectionString("DBConnectionString_Map");
            using (var bulk = new SqlBulkCopy(con) { DestinationTableName = "GoodReportCache" })
            {

                var saveData = list.Select(view => new GoodReportEntity(clientId, view)).ToList();
                var dd = saveData.AsDataReader();
                bulk.WriteToServer(dd);
                
            }
        }

        public GoodReportMapView LoadGoodReport(Guid areaId, GoodReportFilter filter)
        {

            GoodReportMapView views = _ctx.GetDatabase().SqlQuery<GoodReportMapView>("LoadGoodReport " +
                        "@ClientId," +
                        "@AreaId," +
                        "@ShowOrderCount ," +
                        "@ShowSaleCount ," +
                        "@ShowRetSaleCount ," +
                        "@ShowSaleItemCount ," +
                        "@ShowRetSaleItemCount ," +
                        "@ShowSaleQty ," +
                        "@ShowSaleCarton ," +
                        "@ShowRetSaleQty ," +
                        "@ShowRetSaleCarton ," +
                        "@ShowSaleAmount ," +
                        "@ShowRetSaleAmount ," +
                        "@ShowSaleWeight , " +
                        "@ShowRetSaleWeight ," +
                        "@ShowSaleDiscount," +
                        "@ShowRetSaleDiscount ," +
                        "@ShowPrizeCount ," +
                        "@ShowPrizeQty ," +
                        "@ShowPrizeCarton ," +
                        "@HavingCondition, "+
                        "@DefaultFieldIndex ",

            new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier){SqlValue = filter.ClientId},
            new SqlParameter("@AreaId", SqlDbType.UniqueIdentifier){SqlValue = areaId},
            new SqlParameter("@ShowOrderCount", SqlDbType.Bit) { SqlValue = filter.RequestCount },
            new SqlParameter("@ShowSaleCount", SqlDbType.Bit) { SqlValue = filter.FactorCount },
            new SqlParameter("@ShowRetSaleCount", SqlDbType.Bit) { SqlValue = filter.RejectCount },
            new SqlParameter("@ShowSaleItemCount", SqlDbType.Bit) { SqlValue = filter.SaleItemCount },
            new SqlParameter("@ShowRetSaleItemCount", SqlDbType.Bit) { SqlValue = filter.RejectItemCount },
            new SqlParameter("@ShowSaleQty", SqlDbType.Bit) { SqlValue = filter.SaleQty },
            new SqlParameter("@ShowSaleCarton", SqlDbType.Bit) { SqlValue = filter.SaleCarton },
            new SqlParameter("@ShowRetSaleQty", SqlDbType.Bit) { SqlValue = filter.RejectQty },
            new SqlParameter("@ShowRetSaleCarton", SqlDbType.Bit) { SqlValue = filter.RejectCarton },
            new SqlParameter("@ShowSaleAmount", SqlDbType.Bit) { SqlValue = filter.SaleAmount },
            new SqlParameter("@ShowRetSaleAmount", SqlDbType.Bit) { SqlValue = filter.RejectAmount },
            new SqlParameter("@ShowSaleWeight", SqlDbType.Bit) { SqlValue = filter.SaleWeight },
            new SqlParameter("@ShowRetSaleWeight", SqlDbType.Bit) { SqlValue = filter.RejectWeight },
            new SqlParameter("@ShowSaleDiscount", SqlDbType.Bit) { SqlValue = filter.SaleDiscount },
            new SqlParameter("@ShowRetSaleDiscount", SqlDbType.Bit) { SqlValue = filter.RejectDiscount },
            new SqlParameter("@ShowPrizeCount", SqlDbType.Bit) { SqlValue = filter.BonusCount },
            new SqlParameter("@ShowPrizeQty", SqlDbType.Bit) { SqlValue = filter.BonusQty },
            new SqlParameter("@ShowPrizeCarton", SqlDbType.Bit) { SqlValue = filter.BonusCarton },
            new SqlParameter("@HavingCondition", SqlDbType.VarChar, -1) { SqlValue = "" },
            new SqlParameter("@DefaultFieldIndex", SqlDbType.Int) { SqlValue = filter.DefaultField }


                ).FirstOrDefault();

            return views;
        }



        public List<PointView> LoadCustomer( GoodReportCustomerFilter filter)
        {
            var views = _ctx.GetDatabase().SqlQuery<PointView>("LoadGoodReportCustomer " +
                        "@ClientId," +
                        "@Ids ",
            new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier) { SqlValue = filter.ClientId },
            new SqlParameter("@Ids", SqlDbType.VarChar, -1) { SqlValue = GeneralTools.GuidListTostring(filter.AreaIds) }


                ).ToList();

            return views;
        }
    }
}
