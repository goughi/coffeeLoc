using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coffee.Models;
using System.Collections.Generic;

namespace CoffeeMvcTest
{
    [TestClass]
    public class UnitTest1
    {
        CoffeeStore JeongsCafe = new CoffeeStore() { Eircode = "T12GH45", City = City.Dublin, OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1800, Location = "dublin fair city", StoreName = "JeongsCafe", hasWifi = true };
       
        [TestMethod]
        public void TestAddCoffeeStore()
        {
         
            CoffeeStore charleys = new CoffeeStore() { Eircode = "T12GH46", City = City.Dublin, OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1800, Location = "dublin fair city", StoreName = "charleys", hasWifi = true };
            
            Review a = new Review() { CustomerName = "Stevo", ReviewDate =DateTime.Now, CustomerEmail = "stevo@yahoo.com", Eircode = charleys.Eircode, Comment = "It was great", Rating = 4 };
            Review b = new Review() { CustomerName = "john",ReviewDate = DateTime.Now, CustomerEmail = "john@yahoo.com", Eircode = charleys.Eircode, Comment = "It was great", Rating = 3 };
            Review c = new Review() { CustomerName = "pete", ReviewDate = DateTime.Now, CustomerEmail = "pete@yahoo.com", Eircode = charleys.Eircode, Comment = "It wasn't great", Rating = 2 };
         

            Assert.AreEqual(charleys.City, City.Dublin);
            Assert.AreEqual(charleys.OpeningTime, OpeningHour.AM0700);
            Assert.AreEqual(charleys.ClosingTime, ClosingHour.PM1800);
            Assert.AreEqual(charleys.Eircode, "T12GH46");
            Assert.AreEqual(charleys.Location, "dublin fair city");
            Assert.AreEqual(charleys.StoreName, "charleys");
            Assert.AreEqual(charleys.hasWifi, true);
          
          
        }
        [TestMethod]
        public void TestAddReview()
        {
          
          
            CoffeeStore charleys = new CoffeeStore() { Eircode = "T12GH46", City = City.Dublin, OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1800, Location = "dublin fair city", StoreName = "charleys", hasWifi = true };

            Review a = new Review() { CustomerName = "Stevo", ReviewDate = DateTime.Today, CustomerEmail = "stevo@yahoo.com", Eircode = charleys.Eircode, Comment = "It was great", Rating = 4 };
            Review b = new Review() { CustomerName = "john", ReviewDate = DateTime.Today, CustomerEmail = "john@yahoo.com", Eircode = charleys.Eircode, Comment = "It was great", Rating = 3 };
            Review c = new Review() { CustomerName = "pete", ReviewDate = DateTime.Today, CustomerEmail = "pete@yahoo.com", Eircode = charleys.Eircode, Comment = "It wasn't great", Rating = 2 };

            Assert.AreEqual(a.CustomerName, "Stevo");
            Assert.AreEqual(a.CustomerEmail, "stevo@yahoo.com");
            Assert.AreEqual(a.Eircode, charleys.Eircode);
            Assert.AreEqual(a.Rating, 4);
            Assert.AreEqual(a.Comment, "It was great");
           Assert.AreEqual(a.ReviewDate, DateTime.Today);
         

        }

        [TestMethod]
        public void TestIsOpen()
        {
            CoffeeStore charleys = new CoffeeStore() { Eircode = "T12GH46", City = City.Dublin, OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1900, Location = "dublin fair city", StoreName = "charleys", hasWifi = true };
            Assert.AreEqual(charleys.IsOpen, true);
        }

        [TestMethod]
        public void TestAddDrink()
        {
            CoffeeStore charleys = new CoffeeStore() { Eircode = "T12GH46", City = City.Dublin, OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1900, Location = "dublin fair city", StoreName = "charleys", hasWifi = true };
            Drink BubbleTea = new Drink() { DrinkName = "Bubble Tea", Price = 2.45, Eircode = charleys.Eircode };

            Assert.AreEqual(BubbleTea.DrinkName, "Bubble Tea");
            Assert.AreEqual(BubbleTea.Price, 2.45);
            Assert.AreEqual(BubbleTea.Eircode, "T12GH46");
        }

    
    }
}
