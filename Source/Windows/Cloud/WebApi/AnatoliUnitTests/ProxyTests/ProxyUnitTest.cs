using System;
using System.Linq;
using Anatoli.Business;
using Anatoli.Business.Proxy;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Aantoli.Common.Entity.Gateway.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnatoliUnitTests.ProxyTests
{
    [TestClass]
    public class ProxyUnitTest
    {
        [TestMethod]
        public void test1()
        {
            var anatoliProxy = AnatoliProxy<Product, ProductEntity>.Create();

            ProductEntity model = anatoliProxy.Convert(new Product());

            Product DAL_model = anatoliProxy.ReverseConvert(model);

            Assert.IsNotNull(model);

            Assert.IsNotNull(DAL_model);
        }
    }
}
