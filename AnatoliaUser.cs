using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary
{
    public class AnatoliaUser : User
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
        public AnatoliaUser(string parseObjectId, string userName)
        {
            _userId = parseObjectId;
            _username = userName;
        }
        public DateTime BirthDate { get; set; }
        public string ShippingAddress { get; set; }
        public async Task SaveAsync()
        {
            await Task.Run(() => { return; });
            throw new NotImplementedException();
        }
    }
}
