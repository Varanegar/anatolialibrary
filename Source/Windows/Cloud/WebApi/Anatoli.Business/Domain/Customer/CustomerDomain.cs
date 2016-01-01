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
    public class CustomerDomain : BusinessDomain<CustomerViewModel>, IBusinessDomain<Customer, CustomerViewModel>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

        public async Task PublishAsync(List<CustomerViewModel> customerViewModels)
        {
            try
            {
                var customers = Proxy.ReverseConvert(customerViewModels);
                var privateLabelOwner = PrincipalRepository.GetQuery().Where(p => p.Id == PrivateLabelOwnerId).FirstOrDefault();

                customers.ForEach(item =>
                {
                    item.PrivateLabelOwner = privateLabelOwner ?? item.PrivateLabelOwner;
                    var currentCustomer = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Id == item.Id).FirstOrDefault();
                    if (currentCustomer != null)
                    {
                        if (currentCustomer.CustomerCode != item.CustomerCode ||
                                currentCustomer.CustomerName != item.CustomerName ||
                                currentCustomer.Phone != item.Phone ||
                                currentCustomer.Email != item.Email ||
                                currentCustomer.Address != item.Address ||
                                currentCustomer.BirthDay != item.BirthDay ||
                                currentCustomer.Mobile != item.Mobile ||
                                currentCustomer.PostalCode != item.PostalCode ||
                                currentCustomer.NationalCode != item.NationalCode)
                        {
                            currentCustomer.CustomerCode = item.CustomerCode;
                            currentCustomer.CustomerName = item.CustomerName;
                            currentCustomer.Phone = item.Phone;
                            currentCustomer.Email = item.Email;
                            currentCustomer.Address = item.Address;
                            currentCustomer.BirthDay = item.BirthDay;
                            currentCustomer.Mobile = item.Mobile;
                            currentCustomer.PostalCode = item.PostalCode;
                            currentCustomer.NationalCode = item.NationalCode;
                            currentCustomer.LastUpdate = DateTime.Now;
                            Repository.UpdateAsync(currentCustomer);
                        }
                    }
                    else
                    {
                        if(item.Id == null || item.Id == Guid.Empty)
                            item.Id = Guid.NewGuid();
                        item.CreatedDate = item.LastUpdate = DateTime.Now;
                        if (item.CustomerBaskets != null)
                            item = SetBasketData(item, item.CustomerBaskets.ToList(), Repository.DbContext);
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
        }

        public async Task Delete(List<CustomerViewModel> customerViewModels)
        {
            await Task.Factory.StartNew(() =>
            {
                var customers = Proxy.ReverseConvert(customerViewModels);

                customers.ForEach(item =>
                {
                    var product = Repository.GetQuery().Where(p => p.PrivateLabelOwner.Id == PrivateLabelOwnerId && p.Number_ID == item.Number_ID).FirstOrDefault();
                   
                    Repository.DeleteAsync(product);
                });

                Repository.SaveChangesAsync();
            });
        }

        public Customer SetBasketData(Customer data, List<Basket> baskets, AnatoliDbContext context)
        {
            BasketDomain basketDomain = new BasketDomain(data.PrivateLabelOwner.Id, context);
            baskets.ForEach(item =>
            {
                item.PrivateLabelOwner = data.PrivateLabelOwner;
                item.CreatedDate = item.LastUpdate = data.CreatedDate;
                if (item.BasketItems != null)
                    item = SetBasketItemData(item, item.BasketItems.ToList(), context);

                item.Id = Guid.NewGuid();

                data.CustomerBaskets.Add(item);

            });
            return data;
        }


        public Basket SetBasketItemData(Basket basket, List<BasketItem> basketItems, AnatoliDbContext context)
        {
            ProductDomain productDomain = new ProductDomain(basket.PrivateLabelOwner.Id, context);

            basketItems.ToList().ForEach(basketItem =>
            {
                basketItem.PrivateLabelOwner = basket.PrivateLabelOwner;
                basketItem.CreatedDate = basketItem.LastUpdate = basket.CreatedDate;
                basketItem.Id = Guid.NewGuid();
                var product = productDomain.Repository.GetQuery().Where(p => p.Id == basketItem.ProductId).FirstOrDefault();
                if (product != null)
                {
                    basketItem.Product = product;
                    basket.BasketItems.Add(basketItem);
                }
            });
            return basket;
        }
        #endregion
    }
}
