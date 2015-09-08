using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.user
{
    public class AnatoliaUserModel 
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
        public AnatoliaUserModel(string parseObjectId, string userName)
        {
            _userId = parseObjectId;
            _username = userName;
        }
        public DateTime BirthDate { get; set; }
        public string ShippingAddress { get; set; }
        public async Task<bool> SaveAsync()
        {
            await Task.Run(() => { return; });
            throw new NotImplementedException();
        }
        public async Task<bool> LoginAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SendOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
