﻿using System;
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
    public class CustomerDomain : BusinessDomain<CustomerViewModel>, IBusinessDomain<Customer, CustomerViewModel>
    {
        #region Properties
        public IAnatoliProxy<Customer, CustomerViewModel> Proxy { get; set; }
        public IRepository<Customer> Repository { get; set; }
        public IPrincipalRepository PrincipalRepository { get; set; }
        public Guid PrivateLabelOwnerId { get; private set; }

        #endregion

        #region Ctors
        CustomerDomain() { }
        public CustomerDomain(Guid privateLabelOwnerId) : this(privateLabelOwnerId, new AnatoliDbContext()) { }
        public CustomerDomain(Guid privateLabelOwnerId, AnatoliDbContext dbc)
            : this(new CustomerRepository(dbc), new PrincipalRepository(dbc), AnatoliProxy<Customer, CustomerViewModel>.Create())
        {
            PrivateLabelOwnerId = privateLabelOwnerId;
        }
        public CustomerDomain(ICustomerRepository customerRepository, IPrincipalRepository principalRepository, IAnatoliProxy<Customer, CustomerViewModel> proxy)
        {
            Proxy = proxy;
            Repository = customerRepository;
            PrincipalRepository = principalRepository;
        }
        #endregion

        #region Methods
        public async Task<List<CustomerViewModel>> GetAll()
        {
            var customers = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId);

            return Proxy.Convert(customers.ToList()); ;
        }

        public async Task<CustomerViewModel> GetCustomerById(string customerId)
        {
            var customerGuid = Guid.Parse(customerId);
            var customers = await Repository.FindAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Id == customerGuid);

            return Proxy.Convert(customers); ;
        }
        
        public async Task<List<CustomerViewModel>> GetAllChangedAfter(DateTime selectedDate)
        {
            var customers = await Repository.FindAllAsync(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.LastUpdate >= selectedDate);

            return Proxy.Convert(customers.ToList()); ;
        }

        public async Task<List<CustomerViewModel>> PublishAsync(List<CustomerViewModel> dataViewModels)
        {
            try
            {
                Repository.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var customers = Proxy.ReverseConvert(dataViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                customers.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentCustomer = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Id == item.Id).FirstOrDefault();
                    if (currentCustomer != null)
                    {
                        if (currentCustomer.CustomerCode != item.CustomerCode ||
                                currentCustomer.CustomerName != item.CustomerName ||
                                currentCustomer.FirstName != item.FirstName ||
                                currentCustomer.LastName != item.LastName ||
                                currentCustomer.Phone != item.Phone ||
                                currentCustomer.Email != item.Email ||
                                currentCustomer.MainStreet != item.MainStreet ||
                                currentCustomer.OtherStreet != item.OtherStreet ||
                                currentCustomer.BirthDay != item.BirthDay ||
                                currentCustomer.Mobile != item.Mobile ||
                                currentCustomer.DefauleStoreId != item.DefauleStoreId ||
                                currentCustomer.RegionInfoId   != item.RegionInfoId ||
                                currentCustomer.RegionLevel1Id != item.RegionLevel1Id ||
                                currentCustomer.RegionLevel2Id != item.RegionLevel2Id ||
                                currentCustomer.RegionLevel2Id != item.RegionLevel3Id ||
                                currentCustomer.RegionLevel4Id != item.RegionLevel4Id ||
                                currentCustomer.PostalCode != item.PostalCode ||
                                currentCustomer.NationalCode != item.NationalCode)
                        {
                            currentCustomer.CustomerCode = item.CustomerCode;
                            currentCustomer.CustomerName = item.CustomerName;
                            currentCustomer.FirstName = item.FirstName;
                            currentCustomer.LastName = item.LastName;
                            currentCustomer.Phone = item.Phone;
                            currentCustomer.Email = item.Email;
                            currentCustomer.MainStreet = item.MainStreet;
                            currentCustomer.OtherStreet = item.OtherStreet;
                            currentCustomer.BirthDay = item.BirthDay;
                            currentCustomer.Mobile = item.Mobile;
                            currentCustomer.PostalCode = item.PostalCode;
                            currentCustomer.NationalCode = item.NationalCode;
                            currentCustomer.LastUpdate = DateTime.Now;
                            currentCustomer.DefauleStoreId = item.DefauleStoreId;
                            currentCustomer.RegionInfoId = item.RegionInfoId;
                            currentCustomer.RegionLevel1Id= item.RegionLevel1Id;
                            currentCustomer.RegionLevel2Id= item.RegionLevel2Id;
                            currentCustomer.RegionLevel2Id= item.RegionLevel3Id;
                            currentCustomer.RegionLevel4Id = item.RegionLevel4Id;
                            Repository.UpdateAsync(currentCustomer);
                        }
                    }
                    else
                    {
                        if(item.Id == null || item.Id == Guid.Empty)
                            item.Id = Guid.NewGuid();
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

        public async Task<List<CustomerViewModel>> Delete(List<CustomerViewModel> dataViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var customers = Proxy.ReverseConvert(dataViewModels);

                customers.ForEach(item =>
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
