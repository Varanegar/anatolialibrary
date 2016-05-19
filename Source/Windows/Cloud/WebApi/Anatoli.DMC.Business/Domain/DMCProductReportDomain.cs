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


        public List<DMCPointViewModel> LoadProductValueReport(Guid id, DMCProductValueReportFilterModel filter)
        {
            if (filter.ChangeFilter)
            {
                ReloadCacheData(filter);
            }

            return DMCProductReportAdapter.Instance.LoadProductValueReport(id, filter);

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
                SaleOffice = filter.SaleOffice,
                Header = filter.Header,
                Seller = filter.Seller,
                CustomerClass = filter.CustomerClass,
                CustomerActivity = filter.CustomerActivity,
                CustomerDegree = filter.CustomerDegree,
                GoodGroup = filter.GoodGroup,
                DynamicGroup = filter.DynamicGroup,
                Good = filter.Good,
                CommercialName = filter.CommercialName,
                DayCount = filter.DayCount
            };
            var list = new SDSProductReportDomain().ReloadReportData(pmcfilter);
            var dmclist = list.Select(view => new DMCProductReportCacheEntity()
            {
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
                PrizeCarton = view.PrizeCarton
            }).ToList();
            DMCProductReportAdapter.Instance.UpdateReportCache(filter.ClientId, dmclist);

        }
        #endregion

    }
}
