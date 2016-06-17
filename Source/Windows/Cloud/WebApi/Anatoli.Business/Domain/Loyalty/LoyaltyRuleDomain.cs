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
    public class LoyaltyRuleDomain : BusinessDomainV3<LoyaltyRule>, IBusinessDomainV3<LoyaltyRule>
    {
        #region Ctors
        public LoyaltyRuleDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyRuleDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyRule currentLoyaltyRule, LoyaltyRule item)
        {
            if (currentLoyaltyRule != null)
            {
                if (currentLoyaltyRule.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyRule.LastUpdate = DateTime.Now;
                    currentLoyaltyRule.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyRule);
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
