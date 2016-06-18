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
    public class LoyaltyCardSetDomain : BusinessDomainV3<LoyaltyCardSet>, IBusinessDomainV3<LoyaltyCardSet>
    {
        #region Ctors
        public LoyaltyCardSetDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyCardSetDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyCardSet currentLoyaltyCardSet, LoyaltyCardSet item)
        {
            if (currentLoyaltyCardSet != null)
            {
                if (currentLoyaltyCardSet.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyCardSet.LastUpdate = DateTime.Now;
                    currentLoyaltyCardSet.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyCardSet);
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
