using System;
using System.Linq;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.Common.Business;
using Anatoli.Common.Business.Interfaces;

namespace Anatoli.Business.Domain
{
    public class MainProductGroupDomain : BusinessDomainV2<MainProductGroup, MainProductGroupViewModel, MainProductGroupRepository, IMainProductGroupRepository>, IBusinessDomainV2<MainProductGroup, MainProductGroupViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public MainProductGroupDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public MainProductGroupDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }
        #endregion

        #region Methods
        protected override void AddDataToRepository(MainProductGroup currentGroup, MainProductGroup item)
        {
            if (currentGroup != null)
            {
                if (currentGroup.GroupName != item.GroupName ||
                        currentGroup.NLeft != item.NLeft ||
                        currentGroup.NRight != item.NRight ||
                        currentGroup.NLevel != item.NLevel ||
                        currentGroup.ProductGroup2Id != item.ProductGroup2Id)
                {

                    currentGroup.LastUpdate = DateTime.Now;
                    currentGroup.GroupName = item.GroupName;
                    currentGroup.NLeft = item.NLeft;
                    currentGroup.NRight = item.NRight;
                    currentGroup.NLevel = item.NLevel;
                    currentGroup.ProductGroup2Id = item.ProductGroup2Id;
                    MainRepository.Update(currentGroup);
                }
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }

        public async Task<List<MainProductGroup>> FilterMainProductGroupList(string searchTerm)
        {
            var dataList = await MainRepository.GetFromCachedAsync(p => p.DataOwnerId == DataOwnerKey&& p.GroupName.Contains(searchTerm));

            return dataList.ToList();
        }
        #endregion
    }
}
