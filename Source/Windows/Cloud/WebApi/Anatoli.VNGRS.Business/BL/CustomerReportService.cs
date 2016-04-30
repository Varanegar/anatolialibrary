using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingMap.Common.ViewModel;
using TrackingMap.Service.DBManagement;

namespace TrackingMap.Service.BL
{
    public class CustomerReportService
    {
        private readonly IDbContext _ctx;

        public CustomerReportService(IDbContext ctx)           
        {
            _ctx = ctx;          
        }

        public CustomerReportView LoadCustomerReport(Guid? areaid, 
            CustomerReportFilter filter)
        {
            CustomerReportView view;

            SqlParameter areaid_param = new SqlParameter("@AreaId", SqlDbType.UniqueIdentifier);
            areaid_param.SqlValue = areaid;

            SqlParameter typ_param = new SqlParameter("@Type", filter.Type);

            SqlParameter fromdate_param = new SqlParameter("@FromDate", SqlDbType.VarChar);
            fromdate_param.SqlValue = filter.FromDate;

            SqlParameter todate_param = new SqlParameter("@ToDate", SqlDbType.VarChar);
            todate_param.SqlValue = filter.ToDate;

            SqlParameter saleoffice_param = new SqlParameter("@SaleOffice", SqlDbType.UniqueIdentifier);
            if (filter.SaleOffice == null)
                saleoffice_param.SqlValue = DBNull.Value;
            else
                saleoffice_param.SqlValue = filter.SaleOffice;
            saleoffice_param.IsNullable = true;

            SqlParameter header_param = new SqlParameter("@Header", SqlDbType.UniqueIdentifier);
            if (filter.Header == null)
                header_param.SqlValue = DBNull.Value;
            else
                header_param.SqlValue = filter.Header;
            
            SqlParameter seller_param = new SqlParameter("@Seller", SqlDbType.UniqueIdentifier);
            if (filter.Header == null)
                seller_param.SqlValue = DBNull.Value;
            else
                seller_param.SqlValue = filter.Seller;
        
            SqlParameter customerclass_param = new SqlParameter("@CustomerClass", SqlDbType.UniqueIdentifier);
            if (filter.Header == null)
                customerclass_param.SqlValue = DBNull.Value;
            else
                customerclass_param.SqlValue = filter.CustomerClass;
        
            SqlParameter customeractivity_param = new SqlParameter("@CustomerActivity", SqlDbType.UniqueIdentifier);
            if (filter.Header == null)
                customeractivity_param.SqlValue = DBNull.Value;
            else
                customeractivity_param.SqlValue = filter.CustomerActivity;

            SqlParameter customerdegree_param = new SqlParameter("@CustomerDegree", SqlDbType.UniqueIdentifier);
            if (filter.Header == null)
                customerdegree_param.SqlValue = DBNull.Value;
            else
                customerdegree_param.SqlValue = filter.CustomerDegree;
        
            SqlParameter goodgroup_param = new SqlParameter("@GoodGroup", SqlDbType.UniqueIdentifier);
            if (filter.Header == null)
                goodgroup_param.SqlValue = DBNull.Value;
            else
                goodgroup_param.SqlValue = filter.GoodGroup;

            SqlParameter dynamicgroup_param = new SqlParameter("@DynamicGroup", SqlDbType.UniqueIdentifier);
            if (filter.Header == null)
                dynamicgroup_param.SqlValue = DBNull.Value;
            else
                dynamicgroup_param.SqlValue = filter.DynamicGroup;
        
            SqlParameter good_param = new SqlParameter("@Good", SqlDbType.UniqueIdentifier);
            if (filter.Header == null)
                good_param.SqlValue = DBNull.Value;
            else
                good_param.SqlValue = filter.Good;
        
            SqlParameter commercialname_param = new SqlParameter("@CommercialName", SqlDbType.VarChar);
            commercialname_param.SqlValue = filter.CommercialName;
            SqlParameter daycount_param = new SqlParameter("@DayCount", SqlDbType.Int);
            if (filter.DayCount == null)
                daycount_param .SqlValue = DBNull.Value;
            else
                daycount_param .SqlValue = filter.DayCount;
            SqlParameter activecustomercount_param = new SqlParameter("@ActiveCustomerCount", filter.ActiveCustomerCount );
            SqlParameter visitcount_param = new SqlParameter("@VisitCount", filter.VisitCount);
            SqlParameter lackofvisitcount_param = new SqlParameter("@LackOfVisitCount", filter.LackOfVisitCount);
            SqlParameter lackofsalecount_param = new SqlParameter("@LackOfSaleCount", filter.LackOfSaleCount);
            SqlParameter newcustomercount_param = new SqlParameter("@NewCustomerCount", filter.NewCustomerCount);
            SqlParameter duringcheck_param = new SqlParameter("@DuringCheck", filter.DuringCheck);
            SqlParameter rejectcheck_param = new SqlParameter("@RejectCheck", filter.RejectCheck);

            view = _ctx.GetDatabase().SqlQuery<CustomerReportView>("LoadCustomerReport @AreaId," +
                    "@Type,"+ 
                    "@FromDate,"+
                    "@ToDate,"+ 
                    "@SaleOffice,"+
                    "@Header,"+
                    "@Seller,"+
                    "@CustomerClass,"+
                    "@CustomerActivity,"+
			        "@CustomerDegree,"+
                    "@GoodGroup," +
                    "@DynamicGroup,"+
                    "@Good,"+
                    "@CommercialName,"+
                    "@DayCount,"+
                    "@VisitCount,"+
                    "@LackOfVisitCount,"+
                    "@LackOfSaleCount,"+
                    "@NewCustomerCount,"+
                    "@DuringCheck,"+
                    "@RejectCheck",
                    areaid_param,
                    typ_param,
                    fromdate_param,
                    todate_param,
                    saleoffice_param,
                    header_param,
                    seller_param,
                    customerclass_param,
                    customeractivity_param,
                    customerdegree_param,
                    goodgroup_param,
                    dynamicgroup_param,
                    good_param,
                    commercialname_param ,
                    daycount_param ,
                    activecustomercount_param,
                    visitcount_param,
                    lackofvisitcount_param,
                    lackofsalecount_param,
                    newcustomercount_param,
                    duringcheck_param ,
                    rejectcheck_param
            ).FirstOrDefault();

            return view;
        }


    }
}
