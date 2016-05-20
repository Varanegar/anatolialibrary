using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.ViewModels.Gis;
using Anatoli.ViewModels.CommonModels;
using Thunderstruck;

namespace Anatoli.DMC.DataAccess.DataAdapter
{
    public class DMCCompanyPersonelAdapter : DMCBaseAdapter
    {
        #region ctor
        private static DMCCompanyPersonelAdapter instance = null;
        public static DMCCompanyPersonelAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DMCCompanyPersonelAdapter();
                }
                return instance;
            }
        }
        private DMCCompanyPersonelAdapter() { }
        #endregion

        #region method
        public List<SelectListItemViewModel> LoadPersonByGroup(Guid? groupId)
        {
            List<SelectListItemViewModel> result;
            using (var context = GetDataContext(Transaction.No))
            {
                result = context.All<SelectListItemViewModel>("SELECT Title, UniqueId FROM GisCompanyPersonnel2 where GroupUniqueId='" + groupId.ToString() + "'").ToList();
            }
            return result;
        }
        
        #endregion


        public List<SelectListItemViewModel> LoadGroupByArea(Guid? areaId)
        {
            List<SelectListItemViewModel> result;
            using (var context = GetDataContext(Transaction.No))
            {
                result = context.All<SelectListItemViewModel>("SELECT UniqueId, Title  FROM GisCompanyPersonnelSuperviser2 WHERE RegionAreaUniqueId='" + areaId.ToString() + "'").ToList();
            }
            return result;
        }

        public List<DMCPointViewModel> LoadPersonelsProgramPath(string date, List<Guid> personIds)
        {
            var result = new List<DMCPointViewModel>();
            if (personIds.Count > 0)
            {
                var ids = "";
                foreach (var id in personIds)
                {
                    ids += "'" + id.ToString() + "',";
                }

                ids = ids.Remove(ids.Length - 1);
                using (var context = GetDataContext(Transaction.No))
                {
                    result = context.All<DMCPointViewModel>(
                                "SELECT  p.[UniqueId] as [UniqueId]"+
                                    ",[VisitTemplatePathUniqueId] as MasterId"+
                                    ",[Latitude]"+
                                    ",[Longitude] "+
                                "FROM  [VisitProgram] vp "+ 
                                "join [dbo].[GisRegionAreaPoint] p on p.RegionAreaUniqueId = vp.VisitTemplatePathUniqueId "+
                                "WHERE ( DealerUniqueId in ( " + ids + ") ) " +
                                "AND (vp.Date = '"+date+"')" +
                                "ORDER BY [VisitTemplatePathUniqueId], [Priority] " 
                                ).ToList();
                }
            }
            return result;
        }
/*
        public List<DMCPointViewModel> LoadPersonelsAtivities(string date, List<Guid> personIds, bool order, 
            bool lackOrder, bool lackVisit, bool stopWithoutActivity, bool stopWithoutCustomer)
        {
            var result = new List<DMCPointViewModel>();
            if (personIds.Count == 0) return result;

            var ids = personIds.Aggregate("", (current, id) => current + ( id.ToString() + ","));
            ids = ids.Remove(ids.Length - 1);
            using (var context = GetDataContext(Transaction.No))
            {
                result = context.All<DMCPointViewModel>(string.Format(
                    "exec [GisLoadPersonelActivities] '{0}', {1}, {2}, {3}, {4}, {5}",
                    ids, order,lackOrder,lackVisit,stopWithoutCustomer,stopWithoutActivity)
                ).ToList();
            }
            return result;
        }


        public List<DMCPointViewModel> LoadPersonsPath(string date, List<Guid> personIds)
        {
            var result = new List<DMCPointViewModel>();
            if (personIds.Count > 0)
            {
                var ids = "";
                foreach (var id in personIds)
                {
                    ids += "'" + id.ToString() + "',";
                }

                ids = ids.Remove(ids.Length - 1);
                using (var context = GetDataContext(Transaction.No))
                {
                    result = context.All<DMCPointViewModel>("SELECT [UniqueId]" +
                                                            ",[PersonelUniqueId] as MasterId" +
                                                            ",[Latitude]" +
                                                            ",[Longitude] " +
                                                            "FROM [GisPersonelActivity] " +
                                                            "WHERE ( PersonelUniqueId in ( " + ids + ") ) " +
                                                            "ORDER BY [PersonelUniqueId], [DateTime]"
                                                            ).ToList();
                }
            }
            return result;
        }

        */

    }
}
