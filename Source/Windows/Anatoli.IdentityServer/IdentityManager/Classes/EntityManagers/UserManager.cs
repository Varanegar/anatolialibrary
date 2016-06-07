using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Anatoli.IdentityServer.Entities;

namespace Anatoli.IdentityServer.Classes
{
    public class UserManager : UserManager<User, string>
    {
        public UserManager(UserStore store): base (store)
        {
            ClaimsIdentityFactory = new ClaimsFactory();
        }

        public async Task<User> FindByNameOrEmailOrPhoneAsync(string usernameOrEmailOrPhone)
        {
            return await Users.Where(p => (p.Email == usernameOrEmailOrPhone ||
                                           p.PhoneNumber == usernameOrEmailOrPhone ||
                                           p.UserName == usernameOrEmailOrPhone))
                               .FirstOrDefaultAsync();
                                           //p.UserNameStr == usernameOrEmailOrPhone) &&
                                           //p.ApplicationOwnerId == ApplicationOwnerKey &&
                                           //p.DataOwnerId == DataOwnerKey);
        }
    }
}