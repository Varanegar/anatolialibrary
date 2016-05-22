using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.DMC.ViewModels.Gis;

namespace Anatoli.DMC.Business.Domain
{
    public class DMCRouteDomain
    {
        #region Ctors
        public DMCRouteDomain()
        { }
        #endregion

        #region method
        public List<DMCPolyViewModel> LoadAreaSibilingPoints(Guid? id = null)
        {

            var polies = new List<DMCPolyViewModel>();
            var areaService = new DMCVisitTemplatePathDomain();
            var areaPointService = new DMCRegionAreaPointDomain();

                var parentid = areaService.GetParentIdById(id);

                if (parentid != null)
                {
                    var siblingpoints = areaPointService.LoadAreaPointByParentId(parentid, id).ToList();
                    polies = PointListToPolyList(siblingpoints, false);
                }
                foreach (var poly in polies)
                {
                    var view = areaService.GetViewById(poly.MasterId);
                    poly.Lable = view.PathTitle;

                    if ((!view.IsLeaf) && (poly.Points.Count > 0))
                        poly.Points.Add(poly.Points.ElementAt(0));
                }
            return polies;
        }

        public List<DMCPolyViewModel> LoadAreaChildPoints(Guid? id = null)
        {

            var areaService = new DMCVisitTemplatePathDomain();
            var areaPointService = new DMCRegionAreaPointDomain();
            var points = areaPointService.LoadAreaPointByParentId(id).ToList();
            var polies = PointListToPolyList(points, false);
            foreach (var poly in polies)
            {
                var view = areaService.GetViewById(poly.MasterId);
                poly.Lable = view.PathTitle;

                if ((!view.IsLeaf) && (poly.Points.Count > 0))
                    poly.Points.Add(poly.Points.ElementAt(0));
            }
            return polies;
        }

        public bool RemoveAreaPointsByAreaId(Guid guid)
        {

            var areaService = new DMCVisitTemplatePathDomain();
            var areaPointService = new DMCRegionAreaPointDomain();

            var haschild = areaService.HasChild(guid);
            if (haschild)
                return false;
            areaPointService.RemoveByAreaId(guid);

            return true;
        }


        public bool SaveCustomerPosition(List<DMCCustomerPointViewModel> points)
        {
            var customerService = new DMCCustomerDomain();
            var areaPointService = new DMCRegionAreaPointDomain();
            var result = true;
            foreach (var custpnt in points)
            {
                result = customerService.UpdateCustomerLatLng(custpnt);
                if (!result) break;
                result = areaPointService.UpdatePointLatLng(custpnt);
                if (!result) break;
            }
            return result;
        }

 
        public List<DMCPolyViewModel> LoadPersonelsProgramPath(string date, List<Guid> personIds)
        {
            var service = new DMCCompanyPersonelDomain();
            var points = service.LoadPersonelsProgramPath(date, personIds);
            var polies = PointListToPolyList(points, true);
            return polies;
        }

        public List<DMCPointViewModel> LoadCustomerByRouteId(Guid routeId, bool showCustRoute, bool showCustOtherRoute, bool showCustWithOutRoute)
        {
            var areaService = new DMCVisitTemplatePathDomain();
            var areaid = areaService.GetParentIdById(routeId);
            var areacustomerService = new DMCRegionAreaCustomerDomain();
            return areacustomerService.LoadCustomerPointByAreaId(areaid, routeId, showCustRoute, showCustOtherRoute,
                showCustWithOutRoute);
        }

        #endregion


        #region Tools
        private static Color GetRandomColor()
        {
            var randonGen = new Random();
            var c = Color.FromArgb(randonGen.Next(1, 255), randonGen.Next(1, 255),
                                randonGen.Next(1, 255));
            return c;
        }

        private static List<DMCPolyViewModel> PointListToPolyList(List<DMCPointViewModel> list, bool randomColor)
        {
            Guid? group = null;
            var lines = new List<DMCPolyViewModel>();
            var line = new List<DMCPointViewModel>();
            var color = Color.Black;


            foreach (var pointView in list)
            {
                if (group == null)
                    group = pointView.MasterId;

                if (!group.Equals(pointView.MasterId))
                {
                    if (randomColor) color = GetRandomColor();

                    lines.Add(new DMCPolyViewModel()
                    {
                        MasterId = group,
                        Points = line,
                        Color = color.ToArgb().ToString()
                    });
                    line = new List<DMCPointViewModel>();
                    group = pointView.MasterId;
                }
                line.Add(pointView);
            }

            if (randomColor) color = GetRandomColor();
            lines.Add(new DMCPolyViewModel()
            {
                MasterId = group,
                Points = line,
                Color = color.ToArgb().ToString()
            });

            return lines;
        }

        #endregion


    }
}
