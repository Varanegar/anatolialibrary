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
    public class CustomerGroupDomain : BusinessDomainV3<CustomerGroup>, IBusinessDomainV3<CustomerGroup>
    {
        #region Ctors
        public CustomerGroupDomain(OwnerInfo ownerInfo)
            : this(ownerInfo, new AnatoliDbContext())
        {
        }
        public CustomerGroupDomain(OwnerInfo ownerInfo, AnatoliDbContext dbc)
            : base(ownerInfo, dbc)
        {
        }
        #endregion

        #region Methods
        public override void AddDataToRepository(CustomerGroup currentCustomerGroup, CustomerGroup item)
        {
            if (currentCustomerGroup != null)
            {
                if (currentCustomerGroup.GroupName != item.GroupName ||
                    currentCustomerGroup.NLeft != item.NLeft ||
                    currentCustomerGroup.NRight != item.NRight ||
                    currentCustomerGroup.NLevel != item.NLevel ||
                    currentCustomerGroup.CustomerGroup2Id != item.CustomerGroup2Id)
                {
                    currentCustomerGroup.LastUpdate = DateTime.Now;
                    currentCustomerGroup.GroupName = item.GroupName;
                    currentCustomerGroup.NLeft = item.NLeft;
                    currentCustomerGroup.NRight = item.NRight;
                    currentCustomerGroup.NLevel = item.NLevel;
                    currentCustomerGroup.CustomerGroup2Id = item.CustomerGroup2Id;
                    MainRepository.Update(currentCustomerGroup);
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
