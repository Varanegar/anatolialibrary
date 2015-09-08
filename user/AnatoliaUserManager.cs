using AnatoliaLibrary.products;
using AnatoliaLibrary.stores;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.user
{
    public class AnatoliaUserManager
    {
        public static async Task<AnatoliaUserModel> RegisterAsync(string userName, string firstName, string lastName)
        {
            ParseObject anatoliaUser = new ParseObject("AnatoliaUser");
            await anatoliaUser.SaveAsync();
            var user = new AnatoliaUserModel(anatoliaUser.ObjectId, userName);
            user.FirstName = firstName;
            user.LastName = lastName;
            await user.SaveAsync();
            return user;
        }
    }
}
