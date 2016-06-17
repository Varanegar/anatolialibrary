using System;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.StockModels;
using Anatoli.Common.DataAccess.Interfaces;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class StockProductRequestRuleDomain : BusinessDomainV2<StockProductRequestRule, StockProductRequestRuleViewModel, StockProductRequestRuleRepository, IStockProductRequestRuleRepository>, IBusinessDomainV2<StockProductRequestRule, StockProductRequestRuleViewModel>
    {
        #region Properties
        public IRepository<StockProductRequestRuleType> RuleTypeRepository { get; set; }
        public IRepository<StockProductRequestRuleCalcType> RuleCalcTypeRepository { get; set; }
        #endregion

        #region Ctors
        public StockProductRequestRuleDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public StockProductRequestRuleDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
            RuleTypeRepository = new StockProductRequestRuleTypeRepository(dbc);
            RuleCalcTypeRepository = new StockProductRequestRuleCalcTypeRepository(dbc);
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(StockProductRequestRule currentData, StockProductRequestRule item)
        {
            if (currentData != null)
            {
                currentData.StockProductRequestRuleName = item.StockProductRequestRuleName;
                currentData.FromDate = item.FromDate;
                currentData.FromPDate = item.FromPDate;
                currentData.ToDate = item.ToDate;
                currentData.ToPDate = item.ToPDate;
                currentData.ProductId = item.ProductId;
                currentData.ProductTypeId = item.ProductTypeId;
                currentData.MainProductGroupId = item.MainProductGroupId;
                currentData.StockProductRequestRuleTypeId = item.StockProductRequestRuleTypeId;
                currentData.StockProductRequestRuleCalcTypeId = item.StockProductRequestRuleCalcTypeId;
                currentData.Qty = item.Qty;
                currentData.SupplierId = item.SupplierId;
                currentData.ReorderCalcTypeId = item.ReorderCalcTypeId;
                currentData.LastUpdate = DateTime.Now;

                MainRepository.Update(currentData);
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        public async Task<ICollection<StockProductRequestRule>> GetAllValidAsync(DateTime validDate)
        {
            return await MainRepository.FindAllAsync(p => p.DataOwnerId == DataOwnerKey && p.FromDate.CompareTo(validDate) <= 0 && p.ToDate.CompareTo(validDate) >= 0);
        }

        public async Task<ICollection<StockProductRequestRuleType>> GetAllStockProductRequestRuleTypes()
        {
            return await RuleTypeRepository.FindAllAsync(p => p.DataOwnerId == DataOwnerKey);
        }
        public async Task<ICollection<StockProductRequestRuleCalcType>> GetAllStockProductRequestRuleCalcTypes()
        {
            return await RuleCalcTypeRepository.FindAllAsync(p => p.DataOwnerId == DataOwnerKey);
        }
        #endregion
    }
}
