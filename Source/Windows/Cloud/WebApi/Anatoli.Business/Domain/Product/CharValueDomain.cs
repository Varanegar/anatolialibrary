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
    public class CharValueDomain : IBusinessDomain<CharValue, CharValueViewModel>
    {
        #region Properties
        public IAnatoliProxy<CharValue, CharValueViewModel> Proxy { get; set; }
        public IRepository<CharValue> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        CharValueDomain() { }
        public CharValueDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public CharValueDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new CharValueRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<CharValue, CharValueViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public CharValueDomain(ICharValueRepository charValueRepository, IPrincipalRepository principalRepository, IAnatoliProxy<CharValue, CharValueViewModel> proxy)
        {
            Proxy = proxy;
            Repository = charValueRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<CharValueViewModel>> GetAll()
        {
            var charValues = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(charValues.ToList()); ;
        }

        public async Task<List<CharValueViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var charValues = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(charValues.ToList()); ;
        }

        public async Task PublishAsync(List<CharValueViewModel> charViewModels)
        {
            var charValues = Proxy.ReverseConvert(charViewModels);
            var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

            charValues.ForEach(item =>
            {
                item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                var currentCharValue = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                if (currentCharValue != null)
                {
                    currentCharValue.CharValueText = item.CharValueText;
                    currentCharValue.CharValueFromAmount = item.CharValueFromAmount;
                    currentCharValue.CharValueToAmount = item.CharValueToAmount;
                }
                else
                {
                    item.Id = Guid.NewGuid();
                    item.CreatedDate = item.LastUpdate = DateTime.Now;
                }
            });

            await Repository.SaveChangesAsync();
        }

        public async Task Delete(List<CharValueViewModel> charViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var charValues = Proxy.ReverseConvert(charViewModels);

                charValues.ForEach(item =>
                {
                    var charValue = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                   
                    Repository.DeleteAsync(charValue);
                });

                Repository.SaveChangesAsync();
            });
        }
        #endregion
    }
}
