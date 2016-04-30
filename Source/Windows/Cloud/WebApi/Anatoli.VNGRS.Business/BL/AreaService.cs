using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingMap.Service.DBManagement;
using TrackingMap.Service.Entity;
using TrackingMap.Common.ViewModel;

namespace TrackingMap.Service.BL
{
    public class AreaService
    {
        private readonly AreaPointService _areaPointService;
        private readonly IRepository<AreaEntity> _areaRepository;

        public AreaService( 
            IRepository<AreaEntity>  areaRepository,
            AreaPointService areaPointService)
        {
            _areaRepository = areaRepository;
            _areaPointService = areaPointService;
        }

        public Guid? GetParentIdById(Guid? id)
        {
            var area = _areaRepository.GetById(id);
            if (area == null)
                return null;
            return area.ParentId;
        }

        public IList<TextValueView> LoadArea1() // level 1
        {
            var list = _areaRepository.Table.Where(x => x.ParentId == null)
                .Select(x => new TextValueView()
                {
                    Id = x.Id,
                    Title = x.Title,                   
                }).ToList();
            return list;
        }


        public IList<AreaView> LoadAreaByParentId(Guid? id)
        {
            var list = _areaRepository.Table.Where(x => x.ParentId == id)
                .Select(x => new AreaView()
                {
                    Id = x.Id,
                    Title = x.Title,
                    IsLeaf = x.IsLeaf
                }).ToList() ;
            return list;
        }


        public AreaView GetViewById(Guid? id)
        {
            if (id == null)
                return new AreaView();

            return _areaRepository.GetById(id).GetView();
        }

        public List<AreaView> GetAreaPathById(Guid? id)
        {
            var list = new List<AreaView>();
            if (id != null)
            {
                var entity = _areaRepository.GetById(id);
                while (entity.ParentId != null)
                {
                    list.Add(entity.GetView());
                    entity = _areaRepository.GetById(entity.ParentId);

                }
                list.Add(entity.GetView());
            }

            return list;
        }

    }
}
