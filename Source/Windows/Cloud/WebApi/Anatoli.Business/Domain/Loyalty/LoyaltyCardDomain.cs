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
    public class LoyaltyCardDomain : BusinessDomainV3<LoyaltyCard>, IBusinessDomainV3<LoyaltyCard>
    {
        #region Ctors
        public LoyaltyCardDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public LoyaltyCardDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(LoyaltyCard currentLoyaltyCard, LoyaltyCard item)
        {
            if (currentLoyaltyCard != null)
            {
                if (currentLoyaltyCard.IsRemoved != item.IsRemoved)
                {
                    currentLoyaltyCard.LastUpdate = DateTime.Now;
                    currentLoyaltyCard.IsRemoved = item.IsRemoved;
                    MainRepository.Update(currentLoyaltyCard);
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
