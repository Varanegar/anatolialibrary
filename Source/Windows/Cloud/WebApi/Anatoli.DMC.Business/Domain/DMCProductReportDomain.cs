using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.DMC.DataAccess.DataAdapter;
using Anatoli.DMC.DataAccess.Helpers.Entity;
using Anatoli.DMC.ViewModels.Gis;
using Anatoli.DMC.ViewModels.Report;
using Anatoli.SDS.Business.Domain.Gis;
using Anatoli.SDS.ViewModels.GisReport;

namespace Anatoli.DMC.Business.Domain
{
    public class DMCProductReportDomain
    {
        #region Methods
        public List<DMCPolyViewModel> LoadProductReport(DMCProductReportFilterModel filter)
        {

            if (filter.ChangeFilter)
            {
                ReloadCacheData(filter);
            }

            var areaPointService = new DMCRegionAreaPointDomain();

            var polies = new List<DMCPolyViewModel>();
            foreach (Guid id in filter.AreaIds)
            {
                // var view = _areaService.GetViewById(id);
                var points = areaPointService.LoadAreaPointById(id).ToList();
                var rep = DMCProductReportAdapter.Instance.LoadGoodReport(id, filter);

                var poly = new DMCPolyViewModel
                {
                    Points = points,
                    MasterId = id,
                    Desc = rep.Title,
                    Lable = rep.Lable,
                    IsLeaf = rep.IsLeaf,
                    JData = rep.JDesc
                };
                polies.Add(poly);
            }

            return polies;
        }


        public List<DMCPointViewModel> LoadProductValueReport(DMCProductValueReportFilterModel filter)
        {
            if (filter.ChangeFilter)
            {
                ReloadCacheData(filter);
            }
            var points = new List<DMCPointViewModel>();

            if ((filter.AreaIds != null) && (filter.AreaIds.Any()))
                foreach (Guid id in filter.AreaIds)
                {
                    var list = DMCProductReportAdapter.Instance.LoadProductValueReport(id, filter);
                    points.AddRange(list);
                }
            else
            if ((filter.CustomPoint != null) && (filter.CustomPoint.Any()))
            {
                var list = DMCProductReportAdapter.Instance.LoadProductValueReport(null, filter);
                points.AddRange(list);                
            }
            return points;

        }


        public bool RemoveProductReportCache(Guid guid)
        {
            return DMCProductReportAdapter.Instance.RemoveProductReportCache(guid);
        }


        public List<DMCPointViewModel> LoadProductReportCustomer(Guid clientId, List<Guid> areaIds)
        {
            return DMCProductReportAdapter.Instance.LoadCustomer(clientId, areaIds);
        }


        private void ReloadCacheData(DMCProductReportFilterModel filter)
        {
            var pmcfilter = new SDSProductReportFilterModel()
            {
                Type = filter.Type,
                FromDate = filter.FromDate,
                ToDate = filter.ToDate,
                SaleOffice = (filter.SaleOffice ?? "-1"),
                Header = (filter.Header ?? "-1"),
                Seller = (filter.Seller ?? "-1"),
                CustomerClass = (filter.CustomerClass ?? "-1"),
                CustomerActivity = (filter.CustomerActivity ?? "-1"),
                CustomerDegree = (filter.CustomerDegree ?? "-1"),
                GoodGroup = (filter.GoodGroup ?? "-1"),
                DynamicGroup = (filter.DynamicGroup ?? "-1"),
                Good = (filter.Good ?? "-1"),
                CommercialName = filter.CommercialName,
                DayCount = filter.DayCount
            };
            var list = new SDSProductReportDomain().ReloadReportData(pmcfilter);
            var dmclist = list.Select(view => new DMCProductReportCacheEntity()
            {
                UniqueId = Guid.NewGuid(),
                ClientId = filter.ClientId,
                Latitude = view.Latitude / 1000000,
                Longitude = view.Longitude / 1000000,
                OrderCount = view.OrderCount,
                SaleCount = view.SaleCount,
                RetSaleCount = view.RetSaleCount,
                SaleItemCount = view.SaleItemCount,
                RetSaleItemCount = view.RetSaleItemCount,
                SaleAmount = view.SaleAmount,
                RetSaleAmount = view.RetSaleAmount,
                SaleQty = view.SaleQty,
                RetSaleQty = view.RetSaleQty,
                SaleCarton = view.SaleCarton,
                RetSaleCarton = view.RetSaleCarton,
                SaleWeight = view.SaleWeight,
                RetSaleWeight = view.RetSaleWeight,
                SaleDiscount = view.SaleDiscount,
                RetSaleDiscount = view.RetSaleDiscount,
                SalePrizeCount = view.SalePrizeCount,
                PrizeQty = view.PrizeQty,
                PrizeCarton = view.PrizeCarton,
                IntId = view.CustRef
                
            }).ToList();
            DMCProductReportAdapter.Instance.UpdateReportCache(filter.ClientId, dmclist);

        }
        #endregion

    }
}
