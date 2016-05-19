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
 
        public AreaPointService()
        {
        }

        public List<PointView> LoadAreaPointById(Guid? id)
        {
            var areaPointRepository = new EfRepository<AreaPointEntity>();
            var list = areaPointRepository.Table.Where(x => id == null || x.AreaEntityId == id)
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
            var areaPointRepository = new EfRepository<AreaPointEntity>();            
            var m =  areaPointRepository.Table.Where(x => x.AreaEntityId == id).Select(x => (int?) x.Priority).Max() ?? 0;
            if (m == null) m = 0;
            return m;        
        }

        public IList<PointView> LoadAreaPointByParentId(Guid? pId, Guid? id = null )
        {
            var areaRepository = new EfRepository<AreaEntity>();
            var areaPointRepository = new EfRepository<AreaPointEntity>();            

            var q = from area in areaRepository.Table
                    join point in areaPointRepository.Table on area.Id equals point.AreaEntityId
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
            var areaPointRepository = new EfRepository<AreaPointEntity>();            

            var entity = new AreaPointEntity(entityview);
            entity.Priority = GetMaxPointPriorityByAreaId(entityview.AreaId) +1;
            areaPointRepository.Insert(entity);

        }
        internal void RemoveAreaPoint(Guid customerId, Guid areaId)
        {
            var areaPointRepository = new EfRepository<AreaPointEntity>();            

            var list = areaPointRepository.Table.Where(x => x.CustomerEntityId == customerId && x.AreaEntityId == areaId).ToList();
            foreach (var en in list)
            {
                areaPointRepository.Delete(en);
            }
        }


        public ReturnValue SaveAreaPointList(Guid id, List<AreaPointView> entities)
        {
            var areaPointRepository = new EfRepository<AreaPointEntity>();
            var customerAreaRepository = new EfRepository<CustomerAreaEntity>();
            using (var ctx = new MapContext())
            {
                ctx.GetDatabase().ExecuteSqlCommand(string.Format("delete from AreaPoint where AreaId = '{0}'", id));
                ctx.GetDatabase().ExecuteSqlCommand(string.Format("delete from CustomerArea where AreaId = '{0}'", id));
            }
            foreach (var entityview in entities)
            {
                var entity = new AreaPointEntity(entityview);
                entity.AreaEntityId = id;
                entity.IntId = 0;
                areaPointRepository.Insert(entity);
                if (entityview.CstId != null)
                {
                    var custar = new CustomerAreaEntity() { 
                            AreaEntityId = id, 
                            CustomerEntityId = (entityview.CstId ?? Guid.Empty)};
                    customerAreaRepository.Insert(custar);

                }
            }
            return new ReturnValue { Success = true };

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

            var areaRepository = new EfRepository<AreaEntity>();
            var areaPointRepository = new EfRepository<AreaPointEntity>();
            var customerAreaRepository = new EfRepository<CustomerAreaEntity>();

            var ids = areaRepository.Table.Where(x => x.ParentId == id).Select(x => x.Id).ToList();

            if (areaPointRepository.Table.Any(x => ids.Contains(x.Id)))
                return false; //has child


            var list = areaPointRepository.Table.Where(x => x.AreaEntityId == id).ToList();
            foreach(var en in list){
                if (en.CustomerEntityId != null) {
                    var custar = customerAreaRepository.Table.FirstOrDefault(x => x.CustomerEntityId == en.CustomerEntityId && x.AreaEntityId == en.AreaEntityId);
                    customerAreaRepository.Delete(custar);
                }
                areaPointRepository.Delete(en);
            }
            return true;
        }


        public bool HaseAreaPoint(Guid? id)
        {
            var areaPointRepository = new EfRepository<AreaPointEntity>();
            return areaPointRepository.Table.Count(x => x.AreaEntityId == id) > 3;
        }


        #region customer
        public bool AddCustomerToSelected(Guid customerId, Guid areaId)
        {
            var customerAreaRepository = new EfRepository<CustomerAreaEntity>();
            var customerRepository = new EfRepository<CustomerEntity>();

            var custar = new CustomerAreaEntity();
            custar.AreaEntityId = areaId;
            custar.CustomerEntityId = customerId;

            var find = customerAreaRepository.Table
                .FirstOrDefault(x => x.AreaEntityId == areaId && x.CustomerEntityId == customerId);
            if (find == null)
            {
                var cust = customerRepository.GetById(customerId);
                if (cust != null)
                {
                    customerAreaRepository.Insert(custar);
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
            var customerAreaRepository = new EfRepository<CustomerAreaEntity>();

            var custar = customerAreaRepository.Table.FirstOrDefault(x => x.CustomerEntityId == customerId && x.AreaEntityId == areaId);
            customerAreaRepository.Delete(custar);
            RemoveAreaPoint(customerId, areaId);
            return true;
        }

        #endregion


    }
}
