using System;
using System.Threading.Tasks;
using Anatoli.DataAccess.Repositories;
using Anatoli.DataAccess.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnatoliUnitTests
{
    [TestClass]
    public class UserRepositoryUnitTest
    {
        [TestMethod]
        public void AddingUserTestMethod()
        {
            Assert.IsNotNull(AddUser().Result);
        }

        private async Task<User> AddUser()
        {
            var userReposirtory = new UserRepository();
            var private_label = new Principal { Id = Guid.NewGuid(), Title = "Mrg Company" };
            var _id = Guid.NewGuid();
            var model = await userReposirtory.AddAsync(new User
            {
                Id = _id,
                PrivateLabelOwner = private_label,
                AddedBy = private_label,
                Number_ID = 123,
                CreatedDate = DateTime.Now,
                LastEntry = DateTime.Now,
                LastUpdate = DateTime.Now,
                Principal = new Principal { Id = _id, Title = "mrg" },
                FullName = "mrg",
                UserName = "mrg",
            });

            await userReposirtory.SaveChangesAsync();
       
            return model;
        }
    }
}
