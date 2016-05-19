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

        public Guid? GetParentIdById(Guid? id)
        {
            var areaRepository = new EfRepository<AreaEntity>();
            var area = areaRepository.GetById(id);
            if (area == null)
                return null;
            return area.ParentId;
        }

        public IList<TextValueView> LoadArea1() // level 1
        {
            var areaRepository = new EfRepository<AreaEntity>();
            var list = areaRepository.Table.Where(x => x.ParentId == null)
                .Select(x => new TextValueView()
                {
                    Id = x.Id,
                    Title = x.Title,                   
                }).ToList();
            return list;
        }


        public List<AreaView> LoadAreaByParentId(Guid? id)
        {
            var areaRepository = new EfRepository<AreaEntity>();
            var list = areaRepository.Table.Where(x => x.ParentId == id)
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
            var areaRepository = new EfRepository<AreaEntity>();

            return areaRepository.GetById(id).GetView();
        }

        public List<AreaView> GetAreaPathById(Guid? id)
        {
            var areaRepository = new EfRepository<AreaEntity>();
            var list = new List<AreaView>();
            if (id != null)
            {
                var entity = areaRepository.GetById(id);
                while (entity.ParentId != null)
                {
                    list.Add(entity.GetView());
                    entity = areaRepository.GetById(entity.ParentId);

                }
                list.Add(entity.GetView());
            }

            return list;
        }

    }
}
