using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using coffee.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoffeeMvcTest
{
    //testing the list of coffee store
    [TestClass]
    public class CoffeeController
    {
        [TestMethod]
        public void CoffeeList()
        {
            CoffeeController controller = new CoffeeController();
            var result = controller.CoffeeList() as Task(ActionResults);
            Assert.IsNotNull(result);
        }
        //creating coffee store
        [TestMethod]
        public void Create()
        {
            CoffeeController controller = new CoffeeController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }


        //edit coffee store
        [TestMethod]
        public void Edit()
        {
            CoffeeController controller = new CoffeeController();
            var result = controller.Edit(1) as Task<ActionResult>;
            Assert.IsNotNull(result);
        }

        //delete coffee store
        [TestMethod]
        public void Delete()
        {
            CoffeeController controller = new CoffeeController();
            var result = controller.Delete(1) as Task<ActionResult>;
            Assert.IsNotNull(result);
        }

    }
}
