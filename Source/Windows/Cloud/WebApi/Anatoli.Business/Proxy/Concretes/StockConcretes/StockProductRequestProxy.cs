using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.Concretes.StockProductRequestConcretes
{
    public class StockProductRequestProxy : AnatoliProxy<StockProductRequest, StockProductRequestViewModel>, IAnatoliProxy<StockProductRequest, StockProductRequestViewModel>
    {
        public IAnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel> StockProductRequestProductProxy { get; set; }
        #region Ctors
        public StockProductRequestProxy() :
            this(AnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel>.Create()
            )
        { }

        public StockProductRequestProxy(IAnatoliProxy<StockProductRequestProduct, StockProductRequestProductViewModel> stockProductRequestProductProxy
            )
        {
            StockProductRequestProductProxy = stockProductRequestProductProxy;
        }
        #endregion
        public override StockProductRequestViewModel Convert(StockProductRequest data)
        {
            return new StockProductRequestViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,

                RequestDate = data.RequestDate,
                RequestPDate = data.RequestPDate,
                Accept1Date = data.Accept1Date,
                Accept1PDate = data.Accept1PDate,
                Accept1ById = data.Accept1ById,
                Accept2Date = data.Accept2Date,
                Accept2PDate = data.Accept2PDate,
                Accept2ById = data.Accept2ById,
                Accept3Date = data.Accept3Date,
                Accept3PDate = data.Accept3PDate,
                Accept3ById = data.Accept3ById,
                SendToSourceStockDate = data.SendToSourceStockDate,
                SendToSourceStockDatePDate = data.SendToSourceStockDatePDate,
                SourceStockRequestId = data.SourceStockRequestId,

                RequestNo = data.RequestNo,
                SourceStockRequestNo = data.SourceStockRequestNo,

                TargetStockIssueDate = data.TargetStockIssueDate,
                TargetStockIssueDatePDate = data.TargetStockIssueDatePDate,
                TargetStockPaperId = data.TargetStockPaperId,
                TargetStockPaperNo = data.TargetStockPaperNo,
                StockProductRequestStatusId = data.StockProductRequestStatusId,
                StockId = data.StockId,
                SupplyByStockId = data.SupplyByStockId,
                StockTypeId = data.StockTypeId,
                SupplierId = data.SupplierId,
                StockProductRequestSupplyTypeId = data.StockProductRequestSupplyTypeId,
                StockProductRequestTypeId = data.StockProductRequestTypeId,
                ProductTypeId = data.PorductTypeId,
                StockOnHandSyncId = data.StockOnHandSyncId,

                StockProductRequestProducts = (data.StockProductRequestProducts == null) ? null : StockProductRequestProductProxy.Convert(data.StockProductRequestProducts.ToList()),

                AcceptName1 = (data.Accept1By == null)?"":string.Format("{0}  |  {1}", data.Accept1By.Title, data.Accept1PDate),
                AcceptName2 = (data.Accept2By == null) ? "" : string.Format("{0}  |  {1}", data.Accept2By.Title, data.Accept2PDate),
                AcceptName3 = (data.Accept3By == null) ? "" : string.Format("{0}  |  {1}", data.Accept3By.Title, data.Accept3PDate),

                StockProductRequestStatus = data.StockProductRequestStatus.StockProductRequestStatusName,

                StockName = data.Stock != null ? data.Stock.StockName : string.Empty,
                SupplyType = data.StockProductRequestSupplyType != null ? data.StockProductRequestSupplyType.StockProductRequestSupplyTypeName : string.Empty,
                RequestType = data.StockProductRequestType != null ? data.StockProductRequestType.StockProductRequestTypeName : string.Empty,
                SupplyByStock = data.SupplyByStock != null ? data.SupplyByStock.StockName : string.Empty,
                SupplierName = data.Supplier != null ? data.Supplier.SupplierName : string.Empty,
            };
        }

        public override StockProductRequest ReverseConvert(StockProductRequestViewModel data)
        {
            return new StockProductRequest
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ApplicationOwnerId = data.ApplicationOwnerId,

                RequestDate = data.RequestDate,
                RequestPDate = data.RequestPDate,
                Accept1Date = data.Accept1Date,
                Accept1PDate = data.Accept1PDate,
                Accept1ById = data.Accept1ById,
                Accept2Date = data.Accept2Date,
                Accept2PDate = data.Accept2PDate,
                Accept2ById = data.Accept2ById,
                Accept3Date = data.Accept3Date,
                Accept3PDate = data.Accept3PDate,
                Accept3ById = data.Accept3ById,
                SendToSourceStockDate = data.SendToSourceStockDate,
                SendToSourceStockDatePDate = data.SendToSourceStockDatePDate,
                SourceStockRequestId = data.SourceStockRequestId,
                SourceStockRequestNo = data.SourceStockRequestNo,
                TargetStockIssueDate = data.TargetStockIssueDate,
                TargetStockIssueDatePDate = data.TargetStockIssueDatePDate,
                TargetStockPaperId = data.TargetStockPaperId,
                TargetStockPaperNo = data.TargetStockPaperNo,
                StockProductRequestStatusId = data.StockProductRequestStatusId,
                StockId = data.StockId,
                SupplyByStockId = data.SupplyByStockId,
                StockTypeId = data.StockTypeId,
                SupplierId = data.SupplierId,
                StockProductRequestSupplyTypeId = data.StockProductRequestSupplyTypeId,
                StockProductRequestTypeId = data.StockProductRequestTypeId,
                PorductTypeId = data.ProductTypeId,
                StockOnHandSyncId = data.StockOnHandSyncId,

                StockProductRequestProducts = (data.StockProductRequestProducts == null) ? null : StockProductRequestProductProxy.ReverseConvert(data.StockProductRequestProducts),

            };
        }
    }
}