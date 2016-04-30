using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingMap.Common.Tools;
using TrackingMap.Service.DBManagement;
using TrackingMap.Service.Entity;
using TrackingMap.Common.Enum;
using TrackingMap.Service.Tools;
using TrackingMap.Common.ViewModel;
using TrackingMap.Service.ViewModel;

namespace TrackingMap.Service.BL
{
    public class TransactionService
    {
        private readonly IRepository<TransactionEntity> _transactionRepository;
        private readonly IRepository<VisitorEntity> _visitorRepository;
        private readonly IDbContext _ctx;

        public TransactionService(IDbContext ctx,
            IRepository<VisitorEntity> visitorRepository,
            IRepository<TransactionEntity> transactionRepository
            )
        {
            _visitorRepository = visitorRepository;
            _transactionRepository = transactionRepository;
            _ctx = ctx;
        }

        public List<PointView> LoadLastStatus(List<Guid> visitorIds)
        {
            List<PointView> list;
            var str = GeneralTools.GuidListTostring(visitorIds);
            SqlParameter ids_param = new SqlParameter("@VisitorIds", str);

            list = _ctx.GetDatabase().SqlQuery<PointView>("LoadLastStatus @VisitorIds",
                                                          ids_param
                                                          ).ToList();

            return list;
        }


        public List<PointView> LoadTransactionList(List<Guid> visitorIds,
            bool order,
            bool lackOrder,
            bool lackVisit,
            bool stopWithoutCustomer,
            bool stopWithoutActivity)
        {

            //var list = _transactionRepository.Table
            //   .Where(x => visitorIds.Contains(x.VisitorEntityId)
            //         && (   (order && (x.TransactionType == PointType.Order) ) ||
            //                (lackOrder && (x.TransactionType == PointType.LackOfOrder)) ||
            //                (lackVisit && (x.TransactionType == PointType.LackOfVisit)) ||
            //                (stopWithoutCustomer && (x.TransactionType == PointType.StopWithoutCustomer)) ||
            //                (stopWithoutActivity && (x.TransactionType == PointType.StopWithoutActivity))
            //            )
            //   )
            //   .Select(
            //       x =>
            //           new PointView()
            //           {
            //               Id = x.Id,
            //               Desc = x.Desc,
            //               MasterId = x.VisitorEntityId,
            //               Latitude = x.Latitude,
            //               Longitude = x.Longitude,
            //               PointType = x.TransactionType,
            //               SubType = (int)x.CustomerType
            //           }).ToList();

            List<PointView> list;
            var str = GeneralTools.GuidListTostring(visitorIds); 
            SqlParameter ids_param = new SqlParameter("@VisitorIds", str);
            SqlParameter order_param = new SqlParameter("@Order", order);
            SqlParameter lackOrder_param = new SqlParameter("@LackOrder", lackOrder);
            SqlParameter lackVisit_param = new SqlParameter("@LackVisit", lackVisit);
            SqlParameter stopWithoutCustomer_param = new SqlParameter("@StopWithoutCustomer", stopWithoutCustomer);
            SqlParameter stopWithoutActivity_param = new SqlParameter("@StopWithoutActivity", stopWithoutActivity);

            list = _ctx.GetDatabase().SqlQuery<PointView>("LoadVisitorsMarker @VisitorIds," +
                                                          "@Order," +
                                                          "@LackOrder," +
                                                          "@LackVisit," +
                                                          "@StopWithoutCustomer," +
                                                          "@StopWithoutActivity ",
                                                          ids_param,
                                                          order_param,
                                                          lackOrder_param,
                                                          lackVisit_param,
                                                          stopWithoutCustomer_param,
                                                          stopWithoutActivity_param
                                                          ).ToList();

            return list;

        }

    }
}
