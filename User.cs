using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary
{
    public class User
    {
        public static async Task<AnatoliaUser> RegisterAsync(string userName, string firstName, string lastName)
        {
            ParseObject anatoliaUser = new ParseObject("AnatoliaUser");
            await anatoliaUser.SaveAsync();
            var user = new AnatoliaUser(anatoliaUser.ObjectId, userName);
            user.FirstName = firstName;
            user.LastName = lastName;
            await user.SaveAsync();
            return user;
        }
    }
}
