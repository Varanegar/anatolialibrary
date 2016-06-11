using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.DMC.ViewModels.Gis;
using Thunderstruck;
using Anatoli.DMC.DataAccess.Helpers.Entity;

namespace Anatoli.DMC.DataAccess.DataAdapter
{
    public class DMCRegionAreaCustomerAdapter : DMCBaseAdapter
    {
        #region Ctor
        private static DMCRegionAreaCustomerAdapter instance = null;
        public static DMCRegionAreaCustomerAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DMCRegionAreaCustomerAdapter();
                }
                return instance;
            }
        }
        DMCRegionAreaCustomerAdapter() { }
        #endregion
        #region Methods
        public List<DMCRegionAreaCustomerViewModel> LoadCustomerViewByAreaId(Guid? areaid, bool selected)
        {

            List<DMCRegionAreaCustomerViewModel> list;

            //var areaidParam = new SqlParameter("@areaid", areaid);
            //var selectedParam = new SqlParameter("@selected", selected);

            using (var ctx = GetDataContext(Transaction.No))
            {
                list =
                    ctx.All<DMCRegionAreaCustomerViewModel>("exec GisLoadCustomerViewByAreaId " +
                           "'" + areaid + "'," + selected).ToList();
            }
            return list;

        }

        public List<DMCPointViewModel> LoadCustomerPointByAreaId(Guid? areaid, Guid? routeid, bool showcustrout, bool showcustotherrout, bool showcustwithoutrout)
        {
            List<DMCPointViewModel> list;
            using (var ctx = GetDataContext(Transaction.No))
            {
                list =
                    ctx.All<DMCPointViewModel>("exec GisLoadCustomerPointByAreaId " +
                    (areaid == null ? "null" : "'" + areaid + "'") + ", " + (routeid == null ? "null" : "'" + routeid + "'") + "," + showcustrout + "," + showcustotherrout + "," + showcustwithoutrout).ToList();
            }
            return list;
        }

        public bool AddCustomerToRegionArea(Guid customerId, Guid areaId)
        {
            using (var ctx = GetDataContext(Transaction.No))
            {
                try
                {
                    var count = ctx.GetValue<int>(string.Format(
                            "SELECT Count(UniqueId)"+
                            "FROM "+ DMCRegionAreaCustomerEntity.TabelName+" "+
                            "WHERE ( [RegionAreaUniqueId] = '{0}')" +
                            "AND ([CustomerUniqueId] = '{1}')",
                            areaId, customerId));
                    if (count == 0)
                    {
                        var cust = ctx.First<DMCCustomerPointViewModel>(
                            "SELECT  UniqueId, " +
                            "Isnull(Longitude / 1000000.0 , 0) as Longitude, " +
                            "Isnull(Latitude / 1000000.0 ,0) as Latitude " +
                            "FROM Customer " +
                            "WHERE UniqueId = '" + customerId + "'");

                        ctx.Execute(
                            string.Format(DMCRegionAreaCustomerEntity.Insert,
                            areaId,
                            customerId,
                            0
                            ));
                        ctx.Execute(
                            string.Format(DMCRegionAreaPointEntity.Insert,
                            areaId,
                            "'" + customerId +"'",

                            "Isnull((SELECT max(p.Priority) " +
                            "FROM "+DMCRegionAreaPointEntity.TabelName+" P " +
                            "WHERE RegionAreaUniqueId = '" + areaId.ToString() + "'),0)+1",
                            
                            cust.Latitude,
                            cust.Longitude,
                            0
                            ));                            
                        ctx.Commit();
                    }

                }
                catch (Exception e)
                {
                    ctx.Rollback();
                    return false;
                }
            }
            return true;
        }

        public bool RemoveCustomerFromRegionArea(Guid customerId, Guid areaId)
        {
            using (var ctx = GetDataContext())
            {
                try
                {
                    ctx.Execute(string.Format(
                            "DELETE FROM "+ DMCRegionAreaCustomerEntity.TabelName+" "+
                            "WHERE ( [RegionAreaUniqueId] = '{0}')" +
                            "AND ([CustomerUniqueId] = '{1}')",
                            areaId, customerId));

                    ctx.Execute(string.Format(
                            "DELETE FROM " + DMCRegionAreaPointEntity.TabelName + " " +
                            "WHERE ( [RegionAreaUniqueId] = '{0}')" +
                            "AND ([CustomerUniqueId] = '{1}')",
                            areaId, customerId));
                   
                    ctx.Commit();
                }
                catch (Exception e)
                {
                    ctx.Rollback();
                    return false;
                }
            }
            return true;

        }


        public List<DMCRegionAreaCustomerViewModel> LoadCustomerWithoutLocation(Guid areaId)
        {
            List<DMCRegionAreaCustomerViewModel> list;

            //var areaidParam = new SqlParameter("@areaid", areaid);
            //var selectedParam = new SqlParameter("@selected", selected);

            using (var ctx = GetDataContext(Transaction.No))
            {
                var typ =
                    ctx.GetValue<int>(string.Format("select VisitPathTypeId from VisitTemplatePath where UniqueId = '{0}'",
                    areaId));
                var isleaf = typ == 4;

                if (isleaf)
                list =
                    ctx.All<DMCRegionAreaCustomerViewModel>(string.Format(
                    "SELECT customer.UniqueId,"+ 
				        "[Address] as [Desc],"+
                        "'('+[CustomerCode] + ')' +[CustomerName] as CustomerName " +
	                    "FROM customer JOIN 	VisitTemplateCustomer AC ON (customer.UniqueId = AC.CustomerUniqueId) "+
                    "WHERE ([RegionAreaUniqueId] = '{0}') AND (ISNULL(Latitude,0) = 0) AND (ISNULL(Longitude,0) = 0)", areaId)
                    ).ToList();
                else
                list =
                    ctx.All<DMCRegionAreaCustomerViewModel>(
                    "SELECT customer.UniqueId,"+ 
				        "[Address] as [Desc],"+
                        "'('+[CustomerCode] + ')' +[CustomerName] as CustomerName " +
	                "FROM Customer "+
	                "WHERE (ISNULL(Latitude,0) = 0) AND (ISNULL(Longitude,0) = 0)"
                    
                    ).ToList();
            }
            return list;
        }
        public int GetCustomerWithoutLocationCount(Guid areaId)
        {
            List<DMCRegionAreaCustomerViewModel> list;

            //var areaidParam = new SqlParameter("@areaid", areaid);
            //var selectedParam = new SqlParameter("@selected", selected);

            using (var ctx = GetDataContext(Transaction.No))
            {
                var result =
                    ctx.GetValue<int>(string.Format(
                        "SELECT count(customer.UniqueId) " +
                        "FROM customer JOIN VisitTemplateCustomer AC ON (customer.UniqueId = AC.CustomerUniqueId) " +
                        "WHERE ([RegionAreaUniqueId] = '{0}') AND (ISNULL(Latitude,0) = 0) AND (ISNULL(Longitude,0) = 0)",
                        areaId));
                return result;
            }
        }

        public List<DMCRegionAreaCustomerViewModel> LoadCustomerValidLocation(Guid areaId)
        {
            List<DMCRegionAreaCustomerViewModel> list;
            using (var ctx = GetDataContext(Transaction.No))
            {
                list =
                    ctx.All<DMCRegionAreaCustomerViewModel>(string.Format("exec [GisLoadCustomerLocation] @AreaId= '{0}', @SelectedType = 0, @ReturnType = 0 ", areaId)
                    ).ToList();
            }
            return list;
        }

        public List<DMCRegionAreaCustomerViewModel> LoadCustomerInvalidLocation(Guid areaId)
        {
            List<DMCRegionAreaCustomerViewModel> list;
            using (var ctx = GetDataContext(Transaction.No))
            {
                list =
                    ctx.All<DMCRegionAreaCustomerViewModel>(string.Format("exec [GisLoadCustomerLocation] @AreaId= '{0}', @SelectedType =1, @ReturnType = 0 ", areaId)
                    ).ToList();
            }
            return list;
        }

        public int GetCustomerInvalidLocationCount(Guid areaId)
        {
            using (var ctx = GetDataContext(Transaction.No))
            {
                return ctx.GetValue<int>(string.Format("exec [GisLoadCustomerLocation] @AreaId= '{0}', @SelectedType = 1, @ReturnType = 1 ", areaId));
            }
        }
        #endregion


    }
}
