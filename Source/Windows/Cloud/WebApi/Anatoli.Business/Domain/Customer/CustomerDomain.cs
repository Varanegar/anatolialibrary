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
    public class CustomerDomain : BusinessDomainV2<Customer, CustomerViewModel, CustomerRepository, ICustomerRepository>, IBusinessDomainV2<Customer, CustomerViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public CustomerDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public CustomerDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }        
        #endregion

        #region Methods
        protected override void AddDataToRepository(Customer currentCustomer, Customer item)
        {
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
                        currentCustomer.RegionInfoId != item.RegionInfoId ||
                        currentCustomer.RegionLevel1Id != item.RegionLevel1Id ||
                        currentCustomer.RegionLevel2Id != item.RegionLevel2Id ||
                        currentCustomer.RegionLevel3Id != item.RegionLevel3Id ||
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
                    currentCustomer.RegionLevel1Id = item.RegionLevel1Id;
                    currentCustomer.RegionLevel2Id = item.RegionLevel2Id;
                    currentCustomer.RegionLevel3Id = item.RegionLevel3Id;
                    currentCustomer.RegionLevel4Id = item.RegionLevel4Id;
                    MainRepository.Update(currentCustomer);
                }
            }
            else
            {
                if (item.Id == null || item.Id == Guid.Empty)
                    item.Id = Guid.NewGuid();
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }

        #endregion
    }
}

