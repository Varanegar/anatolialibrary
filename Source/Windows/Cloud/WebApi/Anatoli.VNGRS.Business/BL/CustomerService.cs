using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TrackingMap.Service.DBManagement;
using TrackingMap.Service.Entity;
using TrackingMap.Common.ViewModel;
using TrackingMap.Service.ViewModel;

namespace TrackingMap.Service.BL
{
    public class CustomerService
    {
        private readonly IDbContext _ctx;
        private readonly IRepository<CustomerEntity> _customerRepository;
        private readonly IRepository<AreaPointEntity> _areaPointRepository;



        public CustomerService(IDbContext ctx,
            IRepository<CustomerEntity> customerRepository,
            IRepository<AreaPointEntity> areaPointRepository
            )
        {
            _ctx = ctx;
            _customerRepository = customerRepository;
            _areaPointRepository = areaPointRepository;

        }

        public List<PointView> LoadCustomerByAreaId(Guid? areaid, Guid? routid = null,
                bool showcustrout = false,
                bool showcustotherrout = false,
                bool showcustwithoutrout = false)
        {
            List<PointView> list;

            var areaid_param = new SqlParameter("@AreaId", SqlDbType.UniqueIdentifier);
            areaid_param.SqlValue = areaid;

            var routid_param = new SqlParameter("@RoutId", SqlDbType.UniqueIdentifier);
            routid_param.IsNullable = true;
            if (routid == null)
                routid_param.SqlValue = DBNull.Value;
            else
                routid_param.SqlValue = routid;

            var showcustrout_param = new SqlParameter("@ShowCustRout", showcustrout);
            var showcustotherrout_param = new SqlParameter("@ShowCustOtherRout", showcustotherrout);
            var showcustwithoutrout_param = new SqlParameter("@ShowCustEithoutRout", showcustwithoutrout);

            list = _ctx.GetDatabase().SqlQuery<PointView>("LoadCustomerByAreaId @AreaId," +
                                                          "@RoutId," +
                                                          "@ShowCustRout," +
                                                          "@ShowCustOtherRout," +
                                                          "@ShowCustEithoutRout ",
                                                          areaid_param,
                                                          routid_param,
                                                          showcustrout_param,
                                                          showcustotherrout_param,
                                                          showcustwithoutrout_param
                                                          ).ToList();

            return list;
        }

        public List<CustomerView> LoadCustomerSelectedByAreaId(Guid? areaid, bool selected)
        {

            List<CustomerView> list;

            SqlParameter areaid_param = new SqlParameter("@areaid", areaid);
            SqlParameter selected_param = new SqlParameter("@selected", selected);

            list = _ctx.GetDatabase().SqlQuery<CustomerView>("LoadSelectedCustomerByPathId @AreaId, @Selected ", areaid_param, selected_param).ToList();

            return list;

            //List<CustomerView> list;
            //if (selected)
            //{
            //    var q = from cust in _customerRepository.Table
            //        join
            //            are in _customerAreaRepository.Table on cust.Id equals are.CustomerEntityId
            //        where (are.AreaEntityId == areaid)
            //        select new CustomerView() {Id = cust.Id, Title = cust.Title};

            //    list = q.ToList();
            //}
            //else
            //{

            //    var customer_in_area_list = _customerAreaRepository.Table.Where(x => x.AreaEntityId == areaid).ToList();

            //    var q = from cust in _customerRepository.Table
            //            where ( !(customer_in_area_list.Any(x => x.CustomerEntityId == cust.Id)) )
            //            select new CustomerView() { Id = cust.Id, Title = cust.Title };

            //    list = q.ToList();

            //}
            //return list;
        }

        public void SaveCustomerPointList(List<ViewModel.CustomerPointView> points)
        {
            foreach (var cust in points)
            {
                CustomerEntity en;
                en = _customerRepository.GetById(cust.Id);
                if (en == null)
                {
                    en = new CustomerEntity()
                    {
                        Id = cust.Id,
                        Latitude = cust.Lat,
                        Longitude = cust.Lng,
                        Title = "test",
                        IntId = 0
                    };
                    _customerRepository.Insert(en);
                }
                else
                {
                    en.Latitude = cust.Lat;
                    en.Longitude = cust.Lng;
                    _customerRepository.Update(en);
                    var customerpoints = _areaPointRepository.Table.Where(x => x.CustomerEntityId == cust.Id).ToList();
                    foreach (var cp in customerpoints)
                    {
                        cp.Latitude = cust.Lat;
                        cp.Longitude = cust.Lng;
                        _areaPointRepository.Update(cp);
                    }

                }
            }
        }
        public List<VnCustomerView> LoadCustomerAutoComplete(AutoCompleteFilter filter)
        {
            return _customerRepository.TableNoTracking
                .Where(x => (x.Title + " " + x.Code + " " + x.ShopTitle).Contains(filter.SearchValue))
                .Select(x => new VnCustomerView
                {
                    Id = x.Id,
                    CustomerName = x.Code + " " + x.Title,
                    HasLatLng = ((x.Latitude + x.Longitude) != 0)
                })
                .ToList();
        }
    }

}
