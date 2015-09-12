using AnatoliaLibrary.anatoliaclient;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.user
{
    public class AnatoliaUserModel : SyncDataModel
    {
        string _userId;
        public string UserId
        {
            get { return _userId; }
        }
        string _username;
        public string UserName
        {
            get { return _username; }
        }
        string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public AnatoliaUserModel(AnatoliaClient client)
            : base(client)
        {

        }
        public DateTime BirthDate { get; set; }
        List<ShippingInfo> _shippingInfoList;
        public List<ShippingInfo> ShippingInfoList
        {
            get { return _shippingInfoList; }
        }
        public async Task<bool> LoginAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SendOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
        public async Task RegisterAsync(string userName, string firstName, string lastName)
        {
            ParseObject anatoliaUser = new ParseObject("AnatoliaUser");
            await anatoliaUser.SaveAsync();
            _userId = anatoliaUser.ObjectId;
            _username = userName;
            FirstName = firstName;
            LastName = lastName;
            await SaveAsync();
        }
        public async override void LocalSaveAsync()
        {
            var connection = Client.DbClient.GetConnection();
            // todo: Save user object in database
            await Task.Run(() => { return; });
            
        }

        public async override void CloudSaveAsync()
        {
            await Task.Run(() => { return; });

        }
    }
}
