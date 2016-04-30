using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TrackingMap.Service.DBManagement;
using TrackingMap.Service.Entity;
using TrackingMap.Common.ViewModel;
using TrackingMap.Common.Enum;
using TrackingMap.Service.ViewModel;

namespace TrackingMap.Service.BL
{
    public class LastStatusService
    {
        private readonly TransactionService _transactionService;

        public LastStatusService(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public List<PointView> LoadMarkerList(List<Guid> ids)
        {
            var markers = _transactionService.LoadLastStatus(ids);
            return markers;
        }
    }
}
