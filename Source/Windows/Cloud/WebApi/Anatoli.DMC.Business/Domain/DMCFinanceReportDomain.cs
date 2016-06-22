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
    public class DMCFinanceReportDomain
    {
        #region Methods
        public List<DMCPolyViewModel> LoadFinanceReport(DMCFinanceReportFilterModel filter)
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
                var rep = DMCFinanceReportAdapter.Instance.LoadFinanceReport(id, filter);

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



        public List<DMCPointViewModel> LoadFinanceValueReport(DMCFinanceValueReportFilterModel filter)
        {
            if (filter.ChangeFilter)
            {
                ReloadCacheData(filter);
            }
            var points = new List<DMCPointViewModel>();

            if ((filter.AreaIds != null) && (filter.AreaIds.Any()))
                foreach (Guid id in filter.AreaIds)
                {
                    var list = DMCFinanceReportAdapter.Instance.LoadFinanceValueReport(id, filter);
                    points.AddRange(list);
                }
            else
                if ((filter.CustomPoint != null) && (filter.CustomPoint.Any()))
                {
                    var list = DMCFinanceReportAdapter.Instance.LoadFinanceValueReport(null, filter);
                    points.AddRange(list);
                }
            return points;

        }


        public bool RemoveFinanceReportCache(Guid guid)
        {
            return DMCFinanceReportAdapter.Instance.RemoveFinanceReportCache(guid);
        }


        public List<DMCPointViewModel> LoadFinanceReportCustomer(Guid clientId, List<Guid> areaIds)
        {
            return DMCFinanceReportAdapter.Instance.LoadCustomer(clientId, areaIds);
        }


        private void ReloadCacheData(DMCFinanceReportFilterModel filter)
        {
            var pmcfilter = new SDSFinanceReportFilterModel()
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
                UnSoldGood = (filter.UnSoldGood ?? "-1"),
                UnSoldGoodGroup = (filter.UnSoldGoodGroup ?? "-1"),
                DayCount = filter.DayCount
            };
            var list = new SDSFinanceReportDomain().ReloadReportData(pmcfilter);
            var dmclist = list.Select(view => new DMCFinanceReportCacheEntity()
            {
                UniqueId = Guid.NewGuid(),
                ClientId = filter.ClientId,
                Latitude = view.Latitude / 1000000,
                Longitude = view.Longitude / 1000000,
                OpenFactorCount = view.OpenFactorCount,
                OpenFactorAmount = view.OpenFactorAmount,
                OpenFactorDay = view.OpenFactorDay,
                RejectChequeCount = view.RejectChequeCount,
                RejectChequeAmount = view.RejectChequeAmount,
                InprocessChequeCount = view.InprocessChequeCount,
                InprocessChequeAmount = view.InprocessChequeAmount,
                FirstCredit = view.FirstCredit,
                RemainedCredit = view.RemainedCredit,
                FirstDebitCredit = view.FirstDebitCredit,
                RemainedDebitCredit = view.RemainedDebitCredit,
                
                IntId = view.CustRef

            }).ToList();
            RemoveFinanceReportCache(filter.ClientId);
            DMCFinanceReportAdapter.Instance.UpdateReportCache(filter.ClientId, dmclist);

        }
        #endregion

    }
}
