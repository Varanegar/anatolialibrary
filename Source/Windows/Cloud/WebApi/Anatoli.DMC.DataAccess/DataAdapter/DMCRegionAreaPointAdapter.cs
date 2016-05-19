using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.DataAccess.Helpers.Entity;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.DMC.ViewModels.Gis;
using Thunderstruck;

namespace Anatoli.DMC.DataAccess.DataAdapter
{
    public class DMCRegionAreaPointAdapter : DMCBaseAdapter
    {
        #region Ctors
        private static DMCRegionAreaPointAdapter _instance = null;
        public static DMCRegionAreaPointAdapter Instance
        {
            get { return _instance ?? (_instance = new DMCRegionAreaPointAdapter()); }
        }
        private DMCRegionAreaPointAdapter() { }
        #endregion

        #region Methods
        public List<DMCPointViewModel> LoadPointsByAreaId(Guid? id)
        {
            List<DMCPointViewModel> result;
            using (var context = GetDataContext(Transaction.No))
            {
                result =
                context.All<DMCPointViewModel>("SELECT UniqueId, " +
                                               "Priority as Lable, " +
                                               "[RegionAreaUniqueId] as MasterId, " +
                                               "[Latitude],[Longitude]," +
                                               "[CustomerUniqueId] as ReferId " +
                                           "FROM GisRegionAreaPoint " +
                                           "WHERE RegionAreaUniqueId = '" + id.ToString() + "'" + " "+
                                           "ORDER BY Priority "
                                           ).ToList();

            }
            return result;
        }

        public List<DMCPointViewModel> LoadPointsByParentId(Guid? pId, Guid? id = null)
        {
            List<DMCPointViewModel> result;
            using (var context = new DataContext())
            {
                var where = "";
                if (pId != null)
                    where = "WHERE ([ParentUniqueId] = '" + pId.ToString() + "')";

                if (id != null)
                {
                    where += (where == "" ? "WHERE " : "AND");
                    where += string.Format(" (P.UniqueId <> '{0}' )", id);
                }


                result =
                context.All<DMCPointViewModel>("SELECT P.UniqueId, " +
                                               "P.Priority as Lable, " +
                                               "[RegionAreaUniqueId] as MasterId, " +
                                               "[Latitude],[Longitude]," +
                                               "[CustomerUniqueId] as ReferId " +
                                           "FROM  " + DMCRegionAreaPointEntity.TabelName + " AS P JOIN "+
                                                DMCVisitTemplatePathEntity.TabelName +" AS V ON P.[RegionAreaUniqueId] = V.UniqueId "+
                                           where
                                           ).ToList();

            }
            return result;
        }


        public int GetMaxPointPriorityByAreaId(Guid id)
        {
            using (var context = GetDataContext(Transaction.No))
            {
                var result =
                    context.GetValue<int>("SELECT max(Priority) " +
                                          "FROM GisRegionAreaPoint " +
                                          "WHERE RegionAreaUniqueId = '" + id.ToString() + "'"
                        );
                return result;
            }
         }


        public bool HaseAreaPoint(Guid? id)
        {
            using (var context = GetDataContext(Transaction.No))
            {
                var result =
                    context.GetValue<int>("SELECT count(UniqueId) " +
                                          "FROM GisRegionAreaPoint " +
                                          "WHERE RegionAreaUniqueId = '" + id.ToString() + "'"
                        );
                return result > 2;
            }
        }

        public bool SaveAreaPointList(Guid id, List<DMCRegionAreaPointViewModel> points)
        {
            using (var ctx = GetDataContext())
            {
                try
                {
                    ctx.Execute(string.Format(DMCRegionAreaPointEntity.RemoveByAreaId, id));
                    ctx.Execute(string.Format(DMCRegionAreaCustomerEntity.RemoveByAreaId, id));
                    var customerquery = "";
                    var pointquery = "";

                    foreach (var point in points)
                    {
                        pointquery += string.Format(DMCRegionAreaPointEntity.Insert,
                            id,
                            (point.CustomerUniqueId == null ? "null" : "'" + point.CustomerUniqueId + "'"),
                            point.Priority,
                            point.Latitude,
                            point.Longitude,
                            0
                            );
                        if (point.CustomerUniqueId != null)
                        {
                            customerquery += string.Format(DMCRegionAreaCustomerEntity.Insert,
                                id,
                                point.CustomerUniqueId,
                                0
                                );
                        }
                    }
                    if (pointquery != "")
                        ctx.Execute(pointquery);
                    if (customerquery != "")
                        ctx.Execute(customerquery);

                    ctx.Commit();
                }
                catch (Exception e)
                {
                    ctx.Rollback();
                }

            }
            return true;
        }


        #endregion


        public void RemoveByAreaId(Guid id)
        {
            using (var ctx = GetDataContext())
            {
                try { 
                ctx.Execute(string.Format(DMCRegionAreaPointEntity.RemoveByAreaId, id));
                ctx.Execute(string.Format(DMCRegionAreaCustomerEntity.RemoveByAreaId, id));
                ctx.Commit();
                }
                catch (Exception e)
                {
                    ctx.Rollback();
                }

            }
        }

        public bool UpdatePointLatLng(DMCCustomerPointViewModel customerPoint)
        {
            try
            {
                using (var context = GetDataContext(Transaction.No))
                {
                    context.Execute("UPDATE GisRegionAreaPoint " +
                                    "SET  [Latitude] = " + customerPoint.Latitude + "," +
                                         "[Longitude] = " + customerPoint.Longitude + " " +
                                    "WHERE [CustomerUniqueId] ='" + customerPoint.CustomerUniqueId + "'");
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Failed update data ", ex);
                return false;
            }
        }
    }
}
