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

namespace Anatoli.Business.Domain
{
    public class MainProductGroupDomain : BusinessDomain<MainProductGroupViewModel>, IBusinessDomain<MainProductGroup, MainProductGroupViewModel>
    {
        #region Properties
        public IAnatoliProxy<MainProductGroup, MainProductGroupViewModel> Proxy { get; set; }
        public IRepository<MainProductGroup> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        MainProductGroupDomain() { }
        public MainProductGroupDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public MainProductGroupDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new MainProductGroupRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<MainProductGroup, MainProductGroupViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public MainProductGroupDomain(IMainProductGroupRepository MainProductGroupRepository, IPrincipalRepository principalRepository, IAnatoliProxy<MainProductGroup, MainProductGroupViewModel> proxy)
        {
            Proxy = proxy;
            Repository = MainProductGroupRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<MainProductGroupViewModel>> GetAll()
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<MainProductGroupViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var dataList = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(dataList.ToList()); ;
        }

        public async Task<List<MainProductGroupViewModel>> PublishAsync(List<MainProductGroupViewModel> dataViewModels)
        {
            try
            {
                if (dataViewModels.Count == 0) return dataViewModels;

                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;
                var dataList = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();
                var currentGroupList = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId).ToList();

                foreach (MainProductGroup item in dataList)
                {
                    var currentGroup = currentGroupList.Find(t => t.Id == item.Id);
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
                            Repository.Update(currentGroup);
                        }
                    }
                    else
                    {
                        item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        Repository.Add(item);
                    }

                }
                await Repository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            finally
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                log.Info("PublishAsync Finish" + dataViewModels.Count);
            }
            return dataViewModels;

        }

        public async Task<List<MainProductGroupViewModel>> Delete(List<MainProductGroupViewModel> dataViewModels)
        {
            try
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;
                await Task.Factory.StartNew(() =>
                {
                    var dataList = Proxy.ReverseConvert(dataViewModels);

                    dataList.ForEach(item =>
                    {
                        var data = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();

                        Repository.DbContext.MainProductGroups.Remove(data);
                    });

                    Repository.SaveChangesAsync();
                });
            }
            catch (Exception ex)
            {
                log.Error("PublishAsync", ex);
                throw ex;
            }
            finally
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                log.Info("PublishAsync Finish" + dataViewModels.Count);
            }
            return dataViewModels;

        }
        #endregion
    }
}
