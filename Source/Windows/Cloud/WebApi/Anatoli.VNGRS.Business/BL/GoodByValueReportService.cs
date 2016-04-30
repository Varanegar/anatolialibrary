using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingMap.Common.Tools;
using TrackingMap.Common.ViewModel;
using TrackingMap.Service.DBManagement;
using TrackingMap.Service.ViewModel;

namespace TrackingMap.Service.BL
{
    public class GoodByValueReportService
    {
        private readonly IDbContext _ctx;

        public GoodByValueReportService(IDbContext ctx)           
        {
            _ctx = ctx;          
        }

        public List<GoodReportView> LoadGoodByValueReport(Guid areaId, GoodByValueReportFilter filter)
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

            var view = _ctx.GetDatabase().SqlQuery<GoodReportView>("LoadGoodByValueReport " +
                "@ClientId," +
                "@AreaId, " +
                "@HavingCondition ",

                new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier) { SqlValue = filter.ClientId },
                new SqlParameter("@AreaId", SqlDbType.UniqueIdentifier) { SqlValue = areaId },
                new SqlParameter("@HavingCondition", SqlDbType.VarChar, -1) { SqlValue = where }
            ).ToList();


            //var view = _ctx.GetDatabase().SqlQuery<PointView>("LoadGoodByValueReport "+
            //            "@ClientId," +
            //            "@AreaId, " +
            //            "@FromOrderCount ," +
            //            "@ToOrderCount ," +
            //            "@FromSaleCount ," +
            //            "@ToaleCount ," +
            //            "@FromRetSaleCount ," +
            //            "@ToRetSaleCount ," +
            //            "@FromSaleItemCount ," +
            //            "@FromSaleItemCount ," +
            //            "@FromRetSaleItemCount ," +
            //            "@FromRetSaleItemCount ," +
            //            "@FromSaleQty ," +
            //            "@FromSaleQty ," +
            //            "@FromSaleCarton ," +
            //            "@FromSaleCarton ," +
            //            "@FromRetSaleQty ," +
            //            "@FromRetSaleQty ," +
            //            "@FromRetSaleCarton ," +
            //            "@FromRetSaleCarton ," +
            //            "@FromSaleAmount ," +
            //            "@FromSaleAmount ," +
            //            "@FromRetSaleAmount ," +
            //            "@FromRetSaleAmount ," +
            //            "@FromSaleWeight , " +
            //            "@FromSaleWeight , " +
            //            "@FromRetSaleWeight ," +
            //            "@FromRetSaleWeight ," +
            //            "@FromSaleDiscount," +
            //            "@FromSaleDiscount," +
            //            "@FromRetSaleDiscount ," +
            //            "@FromRetSaleDiscount ," +
            //            "@FromPrizeCount ," +
            //            "@FromPrizeCount ," +
            //            "@FromPrizeQty ," +
            //            "@FromPrizeQty ," +
            //            "@FromPrizeCarton ," +
            //            "@FromPrizeCarton ," +
            //            "@HavingCondition ",

            //new SqlParameter("@ClientId", SqlDbType.UniqueIdentifier){SqlValue = filter.ClientId},
            //new SqlParameter("@AreaId", SqlDbType.UniqueIdentifier) { SqlValue = areaId },
            //new SqlParameter("@FromOrderCount", SqlDbType.Int) { SqlValue = filter.RequestCount },
            //new SqlParameter("@FromSaleCount", SqlDbType.Int) { SqlValue = filter.FactorCount },
            //new SqlParameter("@FromRetSaleCount", SqlDbType.Int) { SqlValue = filter.RejectCount },
            //new SqlParameter("@FromSaleItemCount", SqlDbType.Int) { SqlValue = filter.SaleItemCount },
            //new SqlParameter("@FromRetSaleItemCount", SqlDbType.Int) { SqlValue = filter.RejectItemCount },
            //new SqlParameter("@FromSaleQty", SqlDbType.Float) { SqlValue = filter.SaleQty },
            //new SqlParameter("@FromSaleCarton", SqlDbType.Float) { SqlValue = filter.SaleCarton },
            //new SqlParameter("@FromRetSaleQty", SqlDbType.Float) { SqlValue = filter.RejectQty },
            //new SqlParameter("@FromRetSaleCarton", SqlDbType.Float) { SqlValue = filter.RejectCarton },
            //new SqlParameter("@FromSaleAmount", SqlDbType.Decimal) { SqlValue = filter.SaleAmount },
            //new SqlParameter("@FromRetSaleAmount", SqlDbType.Decimal) { SqlValue = filter.RejectAmount },
            //new SqlParameter("@FromSaleWeight", SqlDbType.Float) { SqlValue = filter.SaleWeight },
            //new SqlParameter("@FromRetSaleWeight", SqlDbType.Float) { SqlValue = filter.RejectWeight },
            //new SqlParameter("@FromSaleDiscount", SqlDbType.Decimal) { SqlValue = filter.SaleDiscount },
            //new SqlParameter("@FromRetSaleDiscount", SqlDbType.Decimal) { SqlValue = filter.RejectDiscount },
            //new SqlParameter("@FromPrizeCount", SqlDbType.Int) { SqlValue = filter.FromBonusCount },
            //new SqlParameter("@FromPrizeQty", SqlDbType.Float) { SqlValue = filter.FromBonusQty },
            //new SqlParameter("@FromPrizeCarton", SqlDbType.Float) { SqlValue = filter.FromBonusCarton },
            //new SqlParameter("@HavingCondition", SqlDbType.VarChar, -1) { SqlValue = where }
            //).ToList();

            return view;
        }


    }
}
