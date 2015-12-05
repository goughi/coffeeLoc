using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coffee.Models;
using System.Collections.Generic;

namespace CoffeeMvcTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           // DrinkContext db = new DrinkContext();
            CoffeeStore charleys = new CoffeeStore() { Eircode = "T12GH46", City = City.Dublin, OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1800, Location = "dublin fair city", StoreName = "charleys", hasWifi = true };
        
            Review a = new Review() { CustomerName = "Stevo", ReviewDate =DateTime.Now, CoffeeStore = charleys, CustomerEmail = "stevo@yahoo.com", Eircode = "T12GH46", Comment = "It was great", Rating = 4 };
            Review b = new Review() { CustomerName = "john",ReviewDate = DateTime.Now, CoffeeStore = charleys, CustomerEmail = "john@yahoo.com", Eircode = "T12GH46", Comment = "It was great", Rating = 3 };
            Review c = new Review() { CustomerName = "pete", ReviewDate = DateTime.Now,CoffeeStore = charleys, CustomerEmail = "pete@yahoo.com", Eircode = "T12GH46", Comment = "It wasn't great", Rating = 2 };

            Assert.AreEqual(charleys.StoreRating, 3);
        }
    }
}
