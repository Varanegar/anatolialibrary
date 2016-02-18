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
using Anatoli.ViewModels.CustomerModels;

namespace Anatoli.Business.Domain
{
    public class CustomerShipAddressDomain : BusinessDomain<CustomerShipAddressViewModel>, IBusinessDomain<CustomerShipAddress, CustomerShipAddressViewModel>
    {
        #region Properties
        public IAnatoliProxy<CustomerShipAddress, CustomerShipAddressViewModel> Proxy { get; set; }
        public IRepository<CustomerShipAddress> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        CustomerShipAddressDomain() { }
        public CustomerShipAddressDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public CustomerShipAddressDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new CustomerShipAddressRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<CustomerShipAddress, CustomerShipAddressViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public CustomerShipAddressDomain(ICustomerShipAddressRepository customerRepository, IPrincipalRepository principalRepository, IAnatoliProxy<CustomerShipAddress, CustomerShipAddressViewModel> proxy)
        {
            Proxy = proxy;
            Repository = customerRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<CustomerShipAddressViewModel>> GetAll()
        {
            var customerShipAddresses = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(customerShipAddresses.ToList()); ;
        }

        public async Task<List<CustomerShipAddressViewModel>> GetCustomerShipAddressById(string customerId, bool? isDetault, bool? isActive)
        {
            var customerGuid = Guid.Parse(customerId);
            ICollection<CustomerShipAddress> customerShipAddresses = null;
            if (isDetault == null && isActive == null)
                customerShipAddresses = await Repository.FindAllAsync(p => p.CustomerId == customerGuid);
            else if (isDetault == true)
                customerShipAddresses = await Repository.FindAllAsync(p => p.CustomerId == customerGuid && p.IsDefault);
            else if (isActive == true)
                customerShipAddresses = await Repository.FindAllAsync(p => p.CustomerId == customerGuid && p.IsDefault);

            return Proxy.Convert(customerShipAddresses.ToList());
        }
        public async Task<List<CustomerShipAddressViewModel>> GetCustomerShipAddressByLevel4(string customerId, string levelId)
        {
            var customerGuid = Guid.Parse(customerId);
            var levelGuid = Guid.Parse(levelId);
            var customerShipAddresses = await Repository.FindAllAsync(p => p.CustomerId == customerGuid && p.RegionLevel4Id == levelGuid );

            return Proxy.Convert(customerShipAddresses.ToList());
        }
        
        public async Task<List<CustomerShipAddressViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var customerShipAddresses = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(customerShipAddresses.ToList()); ;
        }

        public async Task<List<CustomerShipAddressViewModel>> PublishAsync(List<CustomerShipAddressViewModel> dataViewModels)
        {
            try
            {
                dataViewModels.ForEach(item =>
                    {
                        if (item.UniqueId == null || item.UniqueId == Guid.Empty)
                            item.UniqueId = Guid.NewGuid();
                    });
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var customerShipAddresses = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                customerShipAddresses.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentCustomerShipAddress = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Id == item.Id).FirstOrDefault();
                    if (currentCustomerShipAddress != null)
                    {
                        currentCustomerShipAddress.Phone = item.Phone;
                        currentCustomerShipAddress.AddressName = item.AddressName;
                        currentCustomerShipAddress.Email = item.Email;
                        currentCustomerShipAddress.MainStreet = item.MainStreet;
                        currentCustomerShipAddress.OtherStreet = item.OtherStreet;
                        currentCustomerShipAddress.Mobile = item.Mobile;
                        currentCustomerShipAddress.PostalCode = item.PostalCode;
                        currentCustomerShipAddress.LastUpdate = DateTime.Now;
                        currentCustomerShipAddress.DefauleStore_Id = item.DefauleStore_Id;
                        currentCustomerShipAddress.RegionInfoId = item.RegionInfoId;
                        currentCustomerShipAddress.RegionLevel1Id = item.RegionLevel1Id;
                        currentCustomerShipAddress.RegionLevel2Id = item.RegionLevel2Id;
                        currentCustomerShipAddress.RegionLevel3Id = item.RegionLevel3Id;
                        currentCustomerShipAddress.RegionLevel4Id = item.RegionLevel4Id;
                        currentCustomerShipAddress.MainStreet = item.MainStreet;
                        currentCustomerShipAddress.OtherStreet = item.OtherStreet;
                        currentCustomerShipAddress.Lat = item.Lat;
                        currentCustomerShipAddress.Lng = item.Lng;
                        currentCustomerShipAddress.CustomerId = item.CustomerId;
                        currentCustomerShipAddress.IsActive = item.IsActive;
                        currentCustomerShipAddress.IsDefault = item.IsDefault;
                        currentCustomerShipAddress.Transferee = item.Transferee;
                        Repository.UpdateAsync(currentCustomerShipAddress);
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
            finally
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = true;
                log.Info("PublishAsync Finish" + dataViewModels.Count);
            }
            return dataViewModels;

        }

        public async Task<List<CustomerShipAddressViewModel>> Delete(List<CustomerShipAddressViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var customerShipAddresses = Proxy.ReverseConvert(dataViewModels);

                customerShipAddresses.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.Id == item.Id).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
            return dataViewModels;
        }

        #endregion
    }
}
