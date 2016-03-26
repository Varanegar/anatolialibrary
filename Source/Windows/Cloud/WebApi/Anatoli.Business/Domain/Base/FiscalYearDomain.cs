using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Domain
{
    public class FiscalYearDomain : BusinessDomainV2<FiscalYear, FiscalYearViewModel, FiscalYearRepository, IFiscalYearRepository>, IBusinessDomainV2<FiscalYear, FiscalYearViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public FiscalYearDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public FiscalYearDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(FiscalYear currentCustomer, FiscalYear item)
        {
            var currentData = MainRepository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
            if (currentData != null)
            {
                if (currentData.FromDate != item.FromDate || currentData.ToDate != item.ToDate)
                {
                    currentData.FromDate = item.FromDate;
                    currentData.ToDate = item.ToDate;
                    currentData.LastUpdate = DateTime.Now;
                    MainRepository.Update(currentData);
                }
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        #endregion
    }
}
