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

namespace Anatoli.Business.Domain
{
    public class StockProductRequestRuleDomain : BusinessDomain<StockProductRequestRuleViewModel>, IBusinessDomain<StockProductRequestRule, StockProductRequestRuleViewModel>
    {
        #region Properties
        public IAnatoliProxy<StockProductRequestRuleType, StockProductRequestRuleTypeViewModel> RuleTypeProxy { get; set; }
        public IAnatoliProxy<StockProductRequestRuleCalcType, StockProductRequestRuleCalcTypeViewModel> RuleCalcTypeProxy { get; set; }
        public IAnatoliProxy<StockProductRequestRule, StockProductRequestRuleViewModel> Proxy { get; set; }

        public IRepository<StockProductRequestRule> Repository { get; set; }
        public IRepository<StockProductRequestRuleType> RuleTypeRepository { get; set; }
        public IRepository<StockProductRequestRuleCalcType> RuleCalcTypeRepository { get; set; }
        #endregion

        #region Ctors
        StockProductRequestRuleDomain() { }
        public StockProductRequestRuleDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public StockProductRequestRuleDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new StockProductRequestRuleRepository(dbc),
                   new PrincipalRepository(dbc),
                   new StockProductRequestRuleTypeRepository(dbc),
                   new StockProductRequestRuleCalcTypeRepository(dbc),
                   AnatoliProxy<StockProductRequestRuleType, StockProductRequestRuleTypeViewModel>.Create(),
                   AnatoliProxy<StockProductRequestRuleCalcType, StockProductRequestRuleCalcTypeViewModel>.Create(),
                   AnatoliProxy<StockProductRequestRule, StockProductRequestRuleViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public StockProductRequestRuleDomain(IStockProductRequestRuleRepository dataRepository,
                                             IPrincipalRepository principalRepository,
                                             IStockProductRequestRuleTypeRepository ruleTypeRepository,
                                             IStockProductRequestRuleCalcTypeRepository ruleCalcTypeRepository,
                                             IAnatoliProxy<StockProductRequestRuleType, StockProductRequestRuleTypeViewModel> ruleTypeProxy,
                                             IAnatoliProxy<StockProductRequestRuleCalcType, StockProductRequestRuleCalcTypeViewModel> ruleCalcTypeProxy,
                                             IAnatoliProxy<StockProductRequestRule, StockProductRequestRuleViewModel> proxy)
        {
            Proxy = proxy;
            Repository = dataRepository;
            PrincipalRepository = principalRepository;
            RuleTypeRepository = ruleTypeRepository;
            RuleCalcTypeRepository = ruleCalcTypeRepository;
            RuleTypeProxy = ruleTypeProxy;
            RuleCalcTypeProxy = ruleCalcTypeProxy;
        }
        #endregion

        #region Methods
        public async Task<List<StockProductRequestRuleViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductRequestRuleViewModel>> GetAll(DateTime validDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.FromDate.CompareTo(validDate) <= 0 && p.ToDate.CompareTo(validDate) >= 0);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductRequestRuleViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<StockProductRequestRuleViewModel>> PublishAsync(List<StockProductRequestRuleViewModel> dataViewModels)
        {
            try
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                dataList.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentData = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                    if (currentData != null)
                    {
                        if (currentData.StockProductRequestRuleName != item.StockProductRequestRuleName)
                        {
                            currentData.StockProductRequestRuleName = item.StockProductRequestRuleName;
                            currentData.FromDate = item.FromDate;
                            currentData.FromPDate = item.FromPDate;
                            currentData.MainProductGroupId = item.MainProductGroupId;
                            currentData.ProductId = item.ProductId;
                            currentData.ProductTypeId = item.ProductTypeId;
                            currentData.ToDate = item.ToDate;
                            currentData.ToPDate = item.ToPDate;

                            currentData.LastUpdate = DateTime.Now;
                            Repository.UpdateAsync(currentData);
                        }
                    }
                    else
                    {
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        Repository.AddAsync(item);
                    }
                });

                await Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            return dataViewModels;
        }

        public async Task<List<StockProductRequestRuleViewModel>> Delete(List<StockProductRequestRuleViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var dataList = Proxy.ReverseConvert(dataViewModels);

                dataList.ForEach(item =>
                {
                    var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                    Repository.DbContext.StockProductRequestRules.Remove(data);
                });

                Repository.SaveChangesAsync();
            });

            return dataViewModels;
        }

        public async Task<List<StockProductRequestRuleTypeViewModel>> GetAllStockProductRequestRuleTypes()
        {
            var model = await RuleTypeRepository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return RuleTypeProxy.Convert(model.ToList());
        }
        public async Task<List<StockProductRequestRuleCalcTypeViewModel>> GetAllStockProductRequestRuleCalcTypes()
        {
            var model = await RuleCalcTypeRepository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return RuleCalcTypeProxy.Convert(model.ToList());
        }
        #endregion
    }
}
