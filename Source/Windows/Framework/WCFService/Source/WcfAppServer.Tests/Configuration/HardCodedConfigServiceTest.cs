using WcfAppServer.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WcfAppServer.Common.Interfaces;
using System.Collections.Generic;

namespace WcfAppServer.Tests
{
    
    
    /// <summary>
    ///This is a test class for HardCodedConfigServiceTest and is intended
    ///to contain all HardCodedConfigServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HardCodedConfigServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        
        /// <summary>
        ///A test for GetAppServerAdmin
        ///</summary>
        [TestMethod()]
        public void GetAppServerAdminTest()
        {
            // arrange

            HardCodedConfigService hardCodedConfigService = new HardCodedConfigService();

            // act

            IWcfServiceConfig actual = hardCodedConfigService.GetAppServerAdmin();

            // assert

            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetWcfServiceConfig1
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void GetWcfServiceConfig1Test()
        {
            // arrange & act 

            IWcfServiceConfig actual = HardCodedConfigService_Accessor.GetWcfServiceConfig1();

            // assert

            Assert.AreEqual(actual.WcfService.ServiceId, "service#1");
        }

        /// <summary>
        ///A test for GetWcfServiceConfig2
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void GetWcfServiceConfig2Test()
        {
            // arrange & act 

            IWcfServiceConfig actual = HardCodedConfigService_Accessor.GetWcfServiceConfig2();

            // assert

            Assert.AreEqual(actual.WcfService.ServiceId, "service#2");
        }

        /// <summary>
        ///A test for GetWcfServiceConfig3
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void GetWcfServiceConfig3Test()
        {
            // arrange & act 

            IWcfServiceConfig actual = HardCodedConfigService_Accessor.GetWcfServiceConfig3();

            // assert

            Assert.AreEqual(actual.WcfService.ServiceId, "service#3");
        }

        /// <summary>
        ///A test for GetWcfServiceLibraries
        ///</summary>
        [TestMethod()]
        public void GetWcfServiceLibrariesTest()
        {
            // arrange
            
            HardCodedConfigService target = new HardCodedConfigService();
            const int expectedCount = 2;
            
            // act

            IList<IWcfServiceLibrary> actual = target.GetWcfServiceLibraries();
            
            // assert

            Assert.AreEqual(expectedCount, actual.Count);
        }
    }
}
