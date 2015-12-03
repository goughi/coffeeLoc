using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coffee.Models;
using coffee.ViewModels;
using System.Data.Entity;


namespace coffee.Controllers
{

    public class CoffeeController : Controller
    {
        private DrinkContext db = new DrinkContext();
        // Do Entity Framework code first
        public ActionResult DoCodeFirst()
        {
            DrinkRepository repository = new DrinkRepository();

            CoffeeStore st1 = new CoffeeStore() { Eircode = "C15C98E", City = City.Limerick, Location = "O' Connell St. Limerick", OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1730, StoreName = "Starbucks", Reviews = new List<Review>(), Drinks = new List<Drink>() };
            CoffeeStore co1 = new CoffeeStore() { Eircode = "C15CK3E", City = City.Limerick, Location = "William st.. Limerick", OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1730, StoreName = "Costa", Reviews = new List<Review>(), Drinks = new List<Drink>() };
            CoffeeStore in1 = new CoffeeStore() { Eircode = "C15C01E", City = City.Limerick, Location = "Cecil St. Limerick", OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1730, StoreName = "Insomnia", Reviews = new List<Review>(), Drinks = new List<Drink>() };
            repository.AddStore(st1);
            repository.AddStore(co1);
            repository.AddStore(in1);


            Drink latte = new Drink() { DrinkName = "Cafe Latte", Price = 3.40, Eircode = co1.Eircode };
            Review r1 = new Review() { CustomerName = "mg1", CustomerEmail = "hllllg@gf.ie", ReviewDate = new DateTime(05 / 05 / 2012), Comment = "The coffee is way too expensive", Rating = 4, Eircode = co1.Eircode };
            Review r2 = new Review() { CustomerName = "mg5", CustomerEmail = "hg85jn@gf.ie", ReviewDate = new DateTime(05 / 05 / 2012), Comment = "The service was shocking", Rating = 4, Eircode = st1.Eircode };
            Review r3 = new Review() { CustomerName = "mj3", CustomerEmail = "hyyydg@gf.ie", ReviewDate = new DateTime(05 / 05 / 2012), Comment = "The coffee was best in Dublin", Rating = 4, Eircode = st1.Eircode };
            Review r4 = new Review() { CustomerName = "keith",CustomerEmail = "hgfdg@gf.ie", ReviewDate = new DateTime(05 / 05 / 2012), Comment = "They had a wide variety of coffees", Rating = 4, Eircode = st1.Eircode };
            repository.AddDrink(latte);
            repository.AddReview(r1);
            repository.AddReview(r2);
            repository.AddReview(r3);
            repository.AddReview(r4);
            Drink Mocha = new Drink() { DrinkName = "Mocha", Price = 3.70, Eircode = st1.Eircode };
            Drink Frappacino = new Drink() { DrinkName = "Frappacino", Price = 4.40, Eircode = st1.Eircode };
            Drink IceCafelatte = new Drink() { DrinkName = "Ice Cafe Latte", Price = 3.40, Eircode = st1.Eircode };
            repository.AddDrink(Mocha);
            repository.AddDrink(Frappacino);
            repository.AddDrink(IceCafelatte);



            ViewBag.Words = new List<string> { "codeFirst", "completed" };
            return View();
        }


        // GET: Coffee list
        public ActionResult CoffeeList(string eircode)
        {
            //string res = eircode
            var viewModel = new CoffeeListData();
            viewModel.coffeeStores = db.CoffeeStores.Include(c => c.Drinks).ToList();
            if (eircode != null)
            {
                ViewBag.Eircode = eircode;
                viewModel.drinks = viewModel.coffeeStores.Where(c => c.Eircode == eircode).Single().Drinks;
            }
            return View(viewModel);
        }
        // GET: Coffee
        public ActionResult StoreIndex(string eircode)
        {
            //string res = eircode
            var viewModel = new CoffeeIndexData();
            viewModel.coffeeStores = db.CoffeeStores.Include(c => c.Reviews).ToList();
           if(eircode!=null)
            {
                ViewBag.Eircode = eircode;
                viewModel.reviews = viewModel.coffeeStores.Where(c => c.Eircode == eircode).Single().Reviews;
            }
            return View(viewModel);
    }
        // GET: CoffeeStores/Details/5
        public ActionResult Details(string eircode)
        {
            if (eircode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeStore coffeeStore = db.CoffeeStores.Find(eircode);
            if (coffeeStore == null)
            {
                return HttpNotFound();
            }
            return View(coffeeStore);
        }

        // GET: CoffeeStores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coffee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Eircode,StoreName,City,Location,hasWifi,OpeningTime,ClosingTime")] CoffeeStore coffeeStore)
        {
            if (ModelState.IsValid)
            {
                db.CoffeeStores.Add(coffeeStore);
                db.SaveChanges();
                return RedirectToAction("StoreIndex");
            }

            return View(coffeeStore);
        }
       
        // GET: Coffee/Edit/5
        public ActionResult Edit(string eircode)
        {
            if (eircode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeStore coffeeStore = db.CoffeeStores.Find(eircode);
            if (coffeeStore == null)
            {
                return HttpNotFound();
            }
            return View(coffeeStore);
        }

    

        // POST: Coffee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Eircode,StoreName,City,Location,hasWifi,OpeningTime,ClosingTime")] CoffeeStore coffeeStore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coffeeStore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("StoreIndex");
            }
            return View(coffeeStore);
        }
     
        // GET: CoffeeStores/Delete/5
        public ActionResult Delete(string eircode)
        {
            if (eircode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeStore coffeeStore = db.CoffeeStores.Find(eircode);
            if (coffeeStore == null)
            {
                return HttpNotFound();
            }
            return View(coffeeStore);
        }

        // POST: CoffeeStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string eircode)
        {
            CoffeeStore coffeeStore = db.CoffeeStores.Find(eircode);
            db.CoffeeStores.Remove(coffeeStore);
            db.SaveChanges();
            return RedirectToAction("StoreIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


