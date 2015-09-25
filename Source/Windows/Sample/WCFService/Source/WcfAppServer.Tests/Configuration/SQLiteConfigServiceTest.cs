using System.Data.SQLite;
using WcfAppServer.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WcfAppServer.Common.Interfaces;
using System.Collections.Generic;

namespace WcfAppServer.Tests
{
    
    
    /// <summary>
    ///This is a test class for SQLiteConfigServiceTest and is intended
    ///to contain all SQLiteConfigServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SQLiteConfigServiceTest
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

            SQLiteConfigService target = new SQLiteConfigService();
            HardCodedConfigService hardCodedConfigService = new HardCodedConfigService();

            IWcfServiceConfig expected = hardCodedConfigService.GetAppServerAdmin();
            IWcfServiceConfig actual;
            
            // act

            actual = target.GetAppServerAdmin();
            
            // assert

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetWcfServiceLibraries
        ///</summary>
        [TestMethod()]
        public void GetWcfServiceLibrariesTest()
        {
            // arrange

            SQLiteConfigService target = new SQLiteConfigService();
            HardCodedConfigService hardCodedConfigService = new HardCodedConfigService();

            IList<IWcfServiceLibrary> expected = hardCodedConfigService.GetWcfServiceLibraries();
            IList<IWcfServiceLibrary> actual;

            // act
            try
            {
                actual = target.GetWcfServiceLibraries();
            }
            catch (SQLiteException e)
            {
                actual = null;
            }
            // assert

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConnectionString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void ConnectionStringTest()
        {
            string actual;
            actual = SQLiteConfigService_Accessor.ConnectionString;
            Assert.IsNotNull(actual);
        }
    }
}
