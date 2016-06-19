using System;
using Anatoli.DataAccess;
using Anatoli.DataAccess.Models;
using Anatoli.DataAccess.Interfaces;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Repositories;
using System.Linq.Expressions;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;
using Anatoli.Common.DataAccess.Models;

namespace Anatoli.Business.Domain
{
    public class LoyaltyRuleActionDomain : BusinessDomainV3<LoyaltyRuleAction>, IBusinessDomainV3<LoyaltyRuleAction>
    {
        #region Ctors
        public LoyaltyRuleActionDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyRuleActionDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyRuleAction currentLoyaltyRuleAction, LoyaltyRuleAction item)
        {
            if (currentLoyaltyRuleAction != null)
            {
                if (currentLoyaltyRuleAction.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyRuleAction.LastUpdate = DateTime.Now;
                    currentLoyaltyRuleAction.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyRuleAction);
                }
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }

        public override void SetConditionForFetchingData()
        {
            MainRepository.ExtraPredicate = p => true;
        }
        #endregion
    }
}
