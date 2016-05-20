using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.DataAccess.Helpers.Entity;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.ViewModels.CommonModels;
using Thunderstruck;

namespace Anatoli.DMC.DataAccess.DataAdapter
{
    public class DMCVisitTemplatePathAdapter : DMCBaseAdapter
    {
        #region Ctors
        private static DMCVisitTemplatePathAdapter _instance = null;
        public static DMCVisitTemplatePathAdapter Instance
        {
            get { return _instance ?? (_instance = new DMCVisitTemplatePathAdapter()); }
        }
        private DMCVisitTemplatePathAdapter() { }
        #endregion

        #region Methods
        public List<DMCVisitTemplatePathViewModel> LoadAreaByParentId(Guid? id)
        {
            List<DMCVisitTemplatePathViewModel> result;
            using (var context = GetDataContext(Transaction.No))
            {
                var where = (id == null ? "" : "WHERE ParentUniqueId = '" + id.ToString() + "'");
                result =
                    context.All<DMCVisitTemplatePathViewModel>("SELECT UniqueId, " +
                                                   "PathTitle, " +
                                                   "IsLeaf, " +
                                                   "ParentUniqueId as ParentId " +
                                               "FROM  " + DMCVisitTemplatePathEntity.TabelName+" "+
                                               where+ " "+
                                               "ORDER BY OrderOf").ToList();
            }
            return result;
        }

        public Guid? GetParentIdById(Guid? id)
        {
            Guid result;
            using (var context = GetDataContext(Transaction.No))
            {
                result =
                    context.GetValue<Guid>("SELECT ParentUniqueId FROM VisitTemplatePath WHERE UniqueId = '" + id.ToString() +
                                           "'");
            }
            return result;
        }

        public List<DMCVisitTemplatePathViewModel> LoadArea1() // level 1
        {
            List<DMCVisitTemplatePathViewModel> result;
            using (var context = GetDataContext(Transaction.No))
            {
                result =
                    context.All<DMCVisitTemplatePathViewModel>("SELECT UniqueId , PathTitle FROM VisitTemplatePath WHERE ParentId =  null").ToList();
            }
            return result;
        }



        public DMCVisitTemplatePathViewModel GetViewById(Guid? id)
        {
            if (id == null)
                return new DMCVisitTemplatePathViewModel();
            DMCVisitTemplatePathViewModel result;
            using (var context = GetDataContext(Transaction.No))
            {
                result =
                    context.First<DMCVisitTemplatePathViewModel>("SELECT UniqueId, " +
                                               "PathTitle, ParentUniqueId," +
                                               "IsLeaf " +
                                               "FROM VisitTemplatePath " +
                                               "WHERE UniqueId = '" + id.ToString() + "'");
            }
            return result;
        }

        public List<DMCVisitTemplatePathViewModel> GetAreaPathById(Guid? id)
        {
            var result = new List<DMCVisitTemplatePathViewModel>();

            if (id != null)
                using (var context = new DataContext())
                {
                    var entity = GetViewById(id);

                    while ((entity != null) && (entity.ParentUniqueId != null))
                    {
                        result.Add(entity);
                        entity = GetViewById(entity.ParentUniqueId);
                    }
                    if (entity != null)
                        result.Add(entity);

                }
            return result;
        }
        #endregion


        public bool HasChild(Guid id)
        {
            using (var context = GetDataContext(Transaction.No))
            {
                var result =
                    context.GetValue<int>("SELECT count(UniqueId) " +
                                          "FROM " + DMCVisitTemplatePathEntity.TabelName+ " "+
                                          "WHERE ParentUniqueId = '" + id.ToString() + "'"
                        );
                return result > 0;
            }
        }

        public List<SelectListItemViewModel> LoadByLevel(int level, Guid? areaId)
        {
            using (var context = GetDataContext(Transaction.No))
            {
                var where = "";
                if (level == 1)
                {
                    where = "WHERE ParentUniqueId Is Null ";
                }
                else
                {
                    where = "WHERE ParentUniqueId = '" + areaId + "' ";
                }

                var result =
                    context.All<SelectListItemViewModel>("SELECT UniqueId, pathTitle as Title " +
                                          "FROM " + DMCVisitTemplatePathEntity.TabelName + " " +
                                          where
                        ).ToList();
                return result;
            }
        }
    }
}
