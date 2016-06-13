using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.DataAccess.DataAdapter;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.DMC.ViewModels.Gis;

namespace Anatoli.DMC.Business.Domain
{
    public class DMCRegionAreaPointDomain
    {
        #region Ctors
        public DMCRegionAreaPointDomain()
        { }
        #endregion
        
        #region Methods
        public List<DMCPointViewModel> LoadAreaPointById(Guid? id)
        {
            return DMCRegionAreaPointAdapter.Instance.LoadPointsByAreaId(id);
        }
        public List<DMCPointViewModel> LoadAreaPointByParentId(Guid? pId, Guid? id = null)
        {

            return DMCRegionAreaPointAdapter.Instance.LoadPointsByParentId(pId, id);

        }

        public int GetMaxPointPriorityByAreaId(Guid id)
        {

            return DMCRegionAreaPointAdapter.Instance.GetMaxPointPriorityByAreaId(id); 
        }


        public DMCPointViewModel GetAreaCenterPointById(Guid id)
        {
            return DMCRegionAreaPointAdapter.Instance.GetAreaCenterPointById(id);
        }

        public bool HaseAreaPoint(Guid? id)
        {
            return DMCRegionAreaPointAdapter.Instance.HaseAreaPoint(id);
        }


        public bool SaveAreaPointList(Guid id, List<DMCRegionAreaPointViewModel> points)
        {
            return DMCRegionAreaPointAdapter.Instance.SaveAreaPointList(id, points);
        }

        public void RemoveByAreaId(Guid id)
        {
            DMCRegionAreaPointAdapter.Instance.RemoveByAreaId(id);
        }


        internal bool UpdatePointLatLng(DMCCustomerPointViewModel custpnt)
        {
            return DMCRegionAreaPointAdapter.Instance.UpdatePointLatLng(custpnt);
        }

        /*


        public void AddAreaPoint(AreaPointView entityview)
        {
            var areaPointRepository = new EfRepository<AreaPointEntity>();

            var entity = new AreaPointEntity(entityview);
            entity.Priority = GetMaxPointPriorityByAreaId(entityview.AreaId) + 1;
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







        #region customer

        #endregion
         */

        #endregion





    }
}
