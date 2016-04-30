using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingMap.Service.DBManagement;
using TrackingMap.Service.Entity;
using TrackingMap.Common.Enum;
using TrackingMap.Common.ViewModel;
using TrackingMap.Service.ViewModel;

namespace TrackingMap.Service.BL
{
    public class AreaPointService
    {
        private readonly IDbContext _ctx;
        private IRepository<AreaPointEntity> _areaPointRepository;
        private IRepository<AreaEntity> _areaRepository;
        private readonly IRepository<CustomerAreaEntity> _customerAreaRepository;
        private readonly IRepository<CustomerEntity> _customerRepository;

        public AreaPointService(IDbContext ctx,
            IRepository<AreaPointEntity>  areaPointRepository,
            IRepository<CustomerEntity> customerRepository,
            IRepository<CustomerAreaEntity> customerAreaRepository,
            IRepository<AreaEntity> areaRepository
            )
        {
            _ctx = ctx;
            _areaRepository = areaRepository;
            _areaPointRepository = areaPointRepository;
            _customerRepository = customerRepository;
            _customerAreaRepository = customerAreaRepository;
        }

        public List<PointView> LoadAreaPointById(Guid? id)
        {
            var list = _areaPointRepository.Table.Where(x => id == null || x.AreaEntityId == id)
                .OrderBy(x => x.Priority)
                .Select(x => new PointView()
                {
                    Id = x.Id,
                    Desc = "",
                    Lable= x.Priority.ToString(),
                    MasterId = x.AreaEntityId,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    PointType = PointType.Point,
                    ReferId = x.CustomerEntityId
                }).ToList() ;
                //IList<PointView> list;
                //var id_param = new SqlParameter("@Id", id);

                //list = ctx.Database.SqlQuery<PointView>("LoadLimits_Point @Id ", id_param).ToList();
                return list;
        }

        public int GetMaxPointPriorityByAreaId(Guid id) {
            var m =_areaPointRepository.Table.Where(x => x.AreaEntityId == id).Select(x => (int?) x.Priority).Max() ?? 0;
            if (m == null) m = 0;
            return m;        
        }

        public IList<PointView> LoadAreaPointByParentId(Guid? pId, Guid? id = null )
        {

            var q = from area in _areaRepository.Table
                    join point in _areaPointRepository.Table on area.Id equals point.AreaEntityId
                    where (area.ParentId == pId)
                    && ((id == null) || (area.Id != id))
                    orderby area.Id ,point.Priority
                    select new PointView()
                            {
                                Id = point.Id,
                                Desc = "",
                                MasterId = point.AreaEntityId,
                                Longitude = point.Longitude,
                                Latitude = point.Latitude,
                                IsLeaf = area.IsLeaf,
                                PointType = PointType.Point
                            };
            //IList<PointView> list;
            //var id_param = new SqlParameter("@Id", id);

            //list = ctx.Database.SqlQuery<PointView>("LoadLimits_Point @Id ", id_param).ToList();
            return q.ToList();
        }

        public void AddAreaPoint(AreaPointView entityview)
        {
            var entity = new AreaPointEntity(entityview);
            entity.Priority = GetMaxPointPriorityByAreaId(entityview.AreaId) +1;
            _areaPointRepository.Insert(entity);

        }
        internal void RemoveAreaPoint(Guid customerId, Guid areaId)
        {
            var list = _areaPointRepository.Table.Where(x => x.CustomerEntityId == customerId && x.AreaEntityId == areaId).ToList();
            foreach (var en in list)
            {
                _areaPointRepository.Delete(en);
            }
        }


        public void SaveAreaPointList(Guid id, List<AreaPointView> entities)
        {
            _ctx.GetDatabase().ExecuteSqlCommand(string.Format("delete from AreaPoint where AreaId = '{0}'", id) );
            _ctx.GetDatabase().ExecuteSqlCommand(string.Format("delete from CustomerArea where AreaId = '{0}'", id));
            foreach (var entityview in entities)
            {
                var entity = new AreaPointEntity(entityview);
                entity.AreaEntityId = id;
                entity.IntId = 0;
                _areaPointRepository.Insert(entity);
                if (entityview.CstId != null)
                {
                    var custar = new CustomerAreaEntity() { 
                            AreaEntityId = id, 
                            CustomerEntityId = (entityview.CstId ?? Guid.Empty)};
                    _customerAreaRepository.Insert(custar);

                }
            }
            

            //Oldversion
                //foreach (var entityview in entities)
                //{

                //    AreaPointEntity entity;
                //    if ((entityview.Id == null) || (entityview.Id.ToString().StartsWith("00000000-0000")))
                //    {
                //        entity = new AreaPointEntity(entityview);
                //        entity.AreaEntityId = id;
                //        entity.IntId = 0;
                //        _areaPointRepository.Insert(entity);
                //    }
                //    else
                //    {
                //        entity = _areaPointRepository.GetById(entityview.Id);
                //        if (entity != null)
                //        {
                //            entity.Priority = entityview.Pr;
                //            entity.Longitude = entityview.Lng;
                //            entity.Latitude = entityview.Lat;
                //        }
                //        _areaPointRepository.Update(entity);
                //    }
                //}
        }

        public bool RemoveAreaPointsByAreaId(Guid? id)
        {

            var ids = _areaRepository.Table.Where(x => x.ParentId == id).Select(x => x.Id).ToList();

            if (_areaPointRepository.Table.Any(x => ids.Contains(x.Id)))
                return false; //has child


            var list = _areaPointRepository.Table.Where(x => x.AreaEntityId == id).ToList();
            foreach(var en in list){
                if (en.CustomerEntityId != null) {
                    var custar = _customerAreaRepository.Table.FirstOrDefault(x => x.CustomerEntityId == en.CustomerEntityId && x.AreaEntityId == en.AreaEntityId);
                    _customerAreaRepository.Delete(custar);
                }
                _areaPointRepository.Delete(en);
            }
            return true;
        }


        public bool HaseAreaPoint(Guid? id)
        {
            return _areaPointRepository.Table.Where(x => x.AreaEntityId == id).Count() > 3;
        }


        #region customer
        public bool AddCustomerToSelected(Guid customerId, Guid areaId)
        {
            var custar = new CustomerAreaEntity();
            custar.AreaEntityId = areaId;
            custar.CustomerEntityId = customerId;

            var find = _customerAreaRepository.Table
                .FirstOrDefault(x => x.AreaEntityId == areaId && x.CustomerEntityId == customerId);
            if (find == null)
            {
                var cust = _customerRepository.GetById(customerId);
                if (cust != null)
                {
                    _customerAreaRepository.Insert(custar);
                    var entityview = new AreaPointView()
                    {
                        AreaId = areaId,
                        CstId = customerId,
                        Lat = cust.Latitude ?? 0,
                        Lng = cust.Longitude ?? 0
                    };
                    AddAreaPoint(entityview);
                }

            }
            return true;
        }

        public bool RemoveCustomerFromSelected(Guid customerId, Guid areaId)
        {
            var custar = _customerAreaRepository.Table.FirstOrDefault(x => x.CustomerEntityId == customerId && x.AreaEntityId == areaId);
            _customerAreaRepository.Delete(custar);
            RemoveAreaPoint(customerId, areaId);
            return true;
        }

        #endregion


    }
}
