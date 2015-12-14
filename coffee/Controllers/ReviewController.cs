using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coffee.Models;
using PagedList;

namespace coffee.Controllers
{
    public class ReviewController : Controller
    {
        private DrinkContext db = new DrinkContext();

        // GET: Reviews/ using filter and sort/ and page numbers
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var reviews = db.Reviews.Include(r => r.CoffeeStore);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Custname_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_desc" : "Rating";
            ViewBag.LocationSortParm = sortOrder == "Location" ? "location_desc" : "Location";
            ViewBag.StoreSortParm = sortOrder == "Store" ? "store_desc" : "Store";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                reviews = reviews.Where(r=> r.CustomerName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    reviews = reviews.OrderByDescending(r => r.CustomerName);
                    break;
                case "Date":
                  reviews = reviews.OrderBy(r => r.ReviewDate);
                    break;
                case "date_desc":
                    reviews = reviews.OrderByDescending(r => r.ReviewDate);
                    break;
                case "Rating":
                    reviews = reviews.OrderBy(r => r.Rating);
                    break;
                case "rating_desc":
                    reviews = reviews.OrderByDescending(r => r.Rating);
                    break;
                case "Location":
                    reviews = reviews.OrderBy(r => r.CoffeeStore.Location);
                    break;
                case "location_desc":
                    reviews = reviews.OrderByDescending(r => r.CoffeeStore.Location);
                    break;
                case "Store":
                    reviews = reviews.OrderBy(r => r.CoffeeStore.StoreName);
                    break;
                case "store_desc":
                    reviews = reviews.OrderByDescending(r => r.CoffeeStore.StoreName);
                    break;
                default:
                    reviews = reviews.OrderBy(r => r.CustomerName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(reviews.ToPagedList(pageNumber, pageSize));

        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Review/ContactCustomer/5
        public ActionResult ContactCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Send Customer a message or promotion!";
            return View(review);
        }

        //send a message/ promotion to customer 
        [HttpPost]
        public ActionResult ContactCustomer(String message, Review review)
        {
            ViewBag.Message = "Message to customer has been sent!";
            return View();
        }
        // GET: Review/Create
        public ActionResult Create()
        {
            Review review = new Review() { Rating = 4, ReviewDate = DateTime.Now };
            ViewBag.Eircode = new SelectList(db.CoffeeStores, "Eircode", "StoreName");
            
            return View(review);
        }

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,CustomerName,CustomerEmail,Comment,Rating,ReviewDate,Eircode")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Eircode = new SelectList(db.CoffeeStores, "Eircode", "StoreName", review.Eircode);
            return View(review);
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.Eircode = new SelectList(db.CoffeeStores, "Eircode", "StoreName", review.Eircode);
            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,CustomerName,CustomerEmail,Comment,Rating,ReviewDate,Eircode")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Eircode = new SelectList(db.CoffeeStores, "Eircode", "StoreName", review.Eircode);
            return View(review);
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
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
