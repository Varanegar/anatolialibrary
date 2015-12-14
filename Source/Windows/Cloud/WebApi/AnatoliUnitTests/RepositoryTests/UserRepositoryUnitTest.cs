using System;
using System.Linq;
using Anatoli.DataAccess;
using System.Threading.Tasks;
using System.Collections.Generic;
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
            Assert.IsNotNull(AddUserThroughRepository().Result);
        }

        private async Task<User> AddUserThroughRepository()
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var dbc = new AnatoliDbContext();
            var principalRepository = new PrincipalRepository(dbc);
            var privateLabelOwner = principalRepository.GetQuery().Where(p => p.Id == owner).FirstOrDefault();

            var userReposirtory = new UserRepository(dbc);
            var _id = Guid.NewGuid();
            var model = await userReposirtory.AddAsync(new User
            {
                Id = _id,
                PrivateLabelOwner = privateLabelOwner,
                AddedBy = privateLabelOwner,
                Number_ID = 123,
                CreatedDate = DateTime.Now,
                LastEntry = DateTime.Now,
                LastUpdate = DateTime.Now,
                Principal = new Principal { Id = _id, Title = "mrg" },
                FullName = "Mohammadreza Gorouhian",
                UserName = "mrg",
                Password = "Sound123",
                Email = "gorouhian@yahoo.com",

            });

            await userReposirtory.SaveChangesAsync();

            return model;
        }

        [TestMethod]
        public void AddingUserTestMethod2()
        {
            Assert.IsNotNull(AddUserThroughUserStore().Result);
        }

        private async Task<User> AddUserThroughUserStore()
        {
            var owner = Guid.Parse("CB11335F-6D14-49C9-9798-AD61D02EDBE1");
            var dbc = new AnatoliDbContext();
            var principalRepository = new PrincipalRepository(dbc);
            var privateLabelOwner = principalRepository.GetQuery().Where(p => p.Id == owner).FirstOrDefault();

            var userReposirtory = new UserRepository(dbc);

            var _id = Guid.NewGuid();
            var model = new User
            {
                Id = _id,
                PrivateLabelOwner = privateLabelOwner,
                AddedBy = privateLabelOwner,
                Number_ID = 123,
                CreatedDate = DateTime.Now,
                LastEntry = DateTime.Now,
                LastUpdate = DateTime.Now,
                Principal = new Principal { Id = _id, Title = "mrg" },
                FullName = "Mohammadreza Gorouhian",
                UserName = "mrg3",
                Password = "Sound123",
                Email = "gorouhian@yahoo.com",

            };

            var userStore = new AnatoliUserStore(userReposirtory);
            //model.Password = await userStore.GetPasswordHashAsync(model);
            await userStore.CreateAsync(model);

            return model;
        }
    }
}
