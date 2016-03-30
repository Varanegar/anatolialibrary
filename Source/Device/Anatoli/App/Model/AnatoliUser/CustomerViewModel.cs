using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model
{
    public class CustomerViewModel : BaseViewModel
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string BirthDay { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string NationalCode { get; set; }
        string _firstName;
        public string FirstName { get { return _firstName == null ? "" : _firstName.Trim(); } set { _firstName = value; } }
        string _lastName;
        public string LastName { get { return _lastName == null ? "" : _lastName.Trim(); } set { _lastName = value; } }
        public string MainStreet { get; set; }
        public string OtherStreet { get; set; }
        public string RegionInfoId { get; set; }
		string _regionLevel1Id;
		string _regionLevel2Id;
		string _regionLevel3Id;
		string _regionLevel4Id;
		public string RegionLevel1Id { get { return (_regionLevel1Id == null) ? null : _regionLevel1Id.ToUpper (); } set{ _regionLevel1Id = value;} }
		public string RegionLevel2Id { get { return (_regionLevel2Id == null) ? null : _regionLevel2Id.ToUpper (); } set { _regionLevel2Id = value;} }
		public string RegionLevel3Id { get { return (_regionLevel3Id == null) ? null : _regionLevel3Id.ToUpper (); } set { _regionLevel3Id = value;} }
		public string RegionLevel4Id { get{ return (_regionLevel4Id == null) ? null : _regionLevel4Id.ToUpper (); } set { _regionLevel4Id = value;} }
        public string DefauleStoreId { get; set; }
        //public List<BasketViewModel> Baskets { get; set; }
    }
}
