
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace CoffeeMvcTest
{
    //unit testing the home controller
    [TestClass]
    public class HomeController
    {
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();

            var result = controller.Index() as Task<ActionResult>;

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void About()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.About() as ViewResult;
            Assert.AreEqual("Coffee Locator.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Contact() as ViewResult;
            Assert.AreEqual("Having trouble? send us a message", result.ViewBag.Message);
        }




        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        //#region Additional test attributes
        ////
        //// You can use the following additional attributes as you write your tests:
        ////
        //// Use ClassInitialize to run code before running the first test in the class
        //// [ClassInitialize()]
        //// public static void MyClassInitialize(TestContext testContext) { }
        ////
        //// Use ClassCleanup to run code after all tests in a class have run
        //// [ClassCleanup()]
        //// public static void MyClassCleanup() { }
        ////
        //// Use TestInitialize to run code before running each test 
        //// [TestInitialize()]
        //// public void MyTestInitialize() { }
        ////
        //// Use TestCleanup to run code after each test has run
        //// [TestCleanup()]
        //// public void MyTestCleanup() { }
        ////
        //#endregion

        //[TestMethod]
        //public void TestMethod1()
        //{
        //    //
        //    // TODO: Add test logic here
        //    //
        //}
    }
}
