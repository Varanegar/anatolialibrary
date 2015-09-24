using System.ServiceModel;
using System.ServiceModel.Description;
using WcfAppServer.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel.Channels;
using System.IO;
using System.Runtime.Serialization;

namespace WcfAppServer.Tests
{
    
    
    /// <summary>
    ///This is a test class for WcfBindingHelperTest and is intended
    ///to contain all WcfBindingHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WcfBindingHelperTest
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
        ///A test for CreateMexHttpBinding
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void CreateMexHttpBindingTest()
        {
            // arrange

            Binding expected = MetadataExchangeBindings.CreateMexHttpBinding();
            Binding actual;
            
            // act 

            actual = WcfBindingHelper_Accessor.CreateMexHttpBinding();
            
            // assert

            Assert.AreSame(expected.GetType(), actual.GetType());
        }

        /// <summary>
        ///A test for CreateMexNamedPipeBinding
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void CreateMexNamedPipeBindingTest()
        {
            // arrange

            Binding expected = MetadataExchangeBindings.CreateMexNamedPipeBinding();
            Binding actual;

            // act 

            actual = WcfBindingHelper_Accessor.CreateMexNamedPipeBinding();

            // assert

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        /// <summary>
        ///A test for CreateMexNetTcpBinding
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void CreateMexNetTcpBindingTest()
        {
            // arrange

            Binding expected = MetadataExchangeBindings.CreateMexTcpBinding();
            Binding actual;

            // act 

            actual = WcfBindingHelper_Accessor.CreateMexNetTcpBinding();

            // assert

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        /// <summary>
        ///A test for CreateNamedPipeBinding
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void CreateNamedPipeBindingTest()
        {
            // arrange

            Binding expected = new NetNamedPipeBinding();
            Binding actual;

            // act 

            actual = WcfBindingHelper_Accessor.CreateNamedPipeBinding();

            // assert

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        /// <summary>
        ///A test for CreateTcpBinding
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void CreateTcpBindingTest()
        {
            // arrange

            Binding expected = new NetTcpBinding();
            Binding actual;

            // act 

            actual = WcfBindingHelper_Accessor.CreateTcpBinding();

            // assert

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        /// <summary>
        ///A test for CreateWebHttpBinding
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void CreateWebHttpBindingTest()
        {
            // arrange

            Binding expected = new WebHttpBinding();
            Binding actual;

            // act 

            actual = WcfBindingHelper_Accessor.CreateWebHttpBinding();

            // assert

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        /// <summary>
        ///A test for CreateWsHttpBinding
        ///</summary>
        [TestMethod()]
        [DeploymentItem("WcfAppServer.Configuration.dll")]
        public void CreateWsHttpBindingTest()
        {
            // arrange

            Binding expected = new WSHttpBinding();
            Binding actual;

            // act 

            actual = WcfBindingHelper_Accessor.CreateWsHttpBinding();

            // assert

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        /// <summary>
        ///A test for Binding Serialization
        ///</summary>
        [TestMethod()]
        public void BindingSerializationTest()
        {
            // arrange

            Stream stream = null;
            Binding expected = WcfBindingHelper_Accessor.CreateTcpBinding();
            Binding actual;

            // act

            stream = WcfBindingHelper.SerializeToStream(expected);
            actual = WcfBindingHelper.Deserialize(stream);
            
            // assert

            Assert.AreSame(expected.GetType(), actual.GetType());
        }


        /// <summary>
        ///A test for GetInferredBinding for NetTcp with no mex
        ///</summary>
        [TestMethod()]
        public void GetInferredBindingTestForNetTcpNoMex()
        {
            // arrange

            string address = @"net.tcp://localhost:1234/test";
            bool isMexBinding = false;
            Binding expected = new NetTcpBinding();
            Binding actual;
            
            // act

            actual = WcfBindingHelper.GetInferredBinding(address, isMexBinding);
            
            // assert

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        /// <summary>
        ///A test for GetInferredBinding for Http with no mex
        ///</summary>
        [TestMethod()]
        public void GetInferredBindingTestForHttpNoMex()
        {
            // arrange

            string address = @"http://localhost:1234/test";
            bool isMexBinding = false;
            Binding expected = new WebHttpBinding();
            Binding actual;

            // act

            actual = WcfBindingHelper.GetInferredBinding(address, isMexBinding);

            // assert

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        /// <summary>
        ///A test for GetInferredBinding for NamedPipe with no mex
        ///</summary>
        [TestMethod()]
        public void GetInferredBindingTestForNamedPipeNoMex()
        {
            // arrange

            string address = @"net.pipe://localhost:1234/test";
            bool isMexBinding = false;
            Binding expected = new NetNamedPipeBinding();
            Binding actual;

            // act

            actual = WcfBindingHelper.GetInferredBinding(address, isMexBinding);

            // assert

            Assert.AreEqual(expected.GetType(), actual.GetType());
        }
    }
}