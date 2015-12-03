using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Coffee.Models
{
    public enum OpeningHour
    {
        [Display(Name = "5:00 AM")]
        AM0500 = 0500, [Display(Name = "6:00 AM")]
        AM0600 = 0600, [Display(Name = "7:00 AM")]
        AM0700 = 0700,
        [Display(Name = "8:00 AM")]
        AM0800 = 0800,
        [Display(Name = "8:30 AM")]
        AM0830 = 0830,
        [Display(Name = "9:00 AM")]
        AM0900 = 0900
    }
    public enum ClosingHour
    {
        [Display(Name = "4:00 PM")]
        PM1600 = 1600,
        [Display(Name = "5:00 PM")]
        PM1700 = 1700,
        [Display(Name = "5:30 PM")]
        PM1730 = 1730,
        [Display(Name = "6:00 PM")]
        PM1800 = 1800,
        [Display(Name = "6:30 PM")]
        PM1830 = 1830,
        [Display(Name = "7:00 PM")]
        PM1900 = 1900
    }
    public enum Rating {[Display(Name = "1")]one = 1, [Display(Name = "2")]two, [Display(Name = "3")]three, [Display(Name = "4")]four, [Display(Name = "5")]five }
    public enum City { Limerick, Cork, Dublin }
    public class CoffeeStore
    {
        //Store name
        [Display(Name = "Store Name")]
        [StringLength(20)]
        [Required(ErrorMessage = " You must enter a name for coffee store")]
        public String StoreName { get; set; }

        //Eircode
        [Key]
        // eircode is a 7 character code, 3 char routing key (A..Z, 1..9)
        // followed by 4 char unique ID (A..Z, 1..9)
        [RegularExpression("([A-Z1-9]{7})", ErrorMessage = "Invalid Eircode")]
        public String Eircode { get; set; }

        //Location
        [Required(ErrorMessage = "You must enter a location")]
        [StringLength(42)]
        public String Location { get; set; }

        //opening/closing hours
        public bool IsOpen
        {
            get
            {
                if ((DateTime.Now.Hour >= (int)OpeningTime) && (DateTime.Now.Hour <= (int)ClosingTime))
                {
                    return true;
                }
                else { return false; }
            }
        }
        [Display(Name = "Opening Time")]
        public OpeningHour OpeningTime { get; set; }

        [Display(Name = "Closing Time")]
        public ClosingHour ClosingTime { get; set; }

        //get overall rating score
        public double StoreRating { get; }

        //City - Limerick, Cork or Dublin
        public City City { get; set; }

        //does store have wifi
        [UIHint("BooleanButtonLabel")]
        public bool hasWifi { get; set; }


        public virtual ICollection<Drink> Drinks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }

    public class Review
    {
        //Review ID
        public int ReviewID { get; set; }

        //Customer's name
        [Display(Name = "Customer's Name")]
        [Required(ErrorMessage = "You must enter a Customer's name")]
        public String CustomerName { get; set; }

        //Customer's email
        [Display(Name = "Customer Email Address")]
        [Required(ErrorMessage = "you must enter an email address")]
        [EmailAddress(ErrorMessage = "you must enter a valid email address")]
        public String CustomerEmail { get; set; }

        //Make a comment
        [Required(ErrorMessage = "You must enter a comment")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "comments range from 10 to 100 characters")]
        public String Comment { get; set; }

        //Rate the store 1 = bad .... 5 = excellent
        [UIHint("_StarRating")]
        public int Rating { get; set; }

        //Review date
        [Display(Name = "Review Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? ReviewDate { get; set; }

        //Eircode is the foreign key
        public String Eircode { get; set; }
        public virtual CoffeeStore CoffeeStore { get; set; }
    }
    public class Drink
    {
        [Key]
        public int DrinkID { get; set; }

        //Drink Name
        [Display(Name = "Drink")]
        [StringLength(20)]
        [Required(ErrorMessage = "You must enter a drink name")]
        public String DrinkName { get; set; }

        //coffee price
        [Range(1.0, 10, ErrorMessage = "Price must be between 1 and 10 euros")]
        public double Price { get; set; }

        public String Eircode { get; set; }
        public virtual CoffeeStore CoffeeStore { get; set; }

    }
    public class DrinkContext : DbContext
    {
        public DrinkContext() : base("DefaultConnection")
        {
            Database.SetInitializer<DrinkContext>(new DropCreateDatabaseIfModelChanges<DrinkContext>());
        }
        public DbSet<CoffeeStore> CoffeeStores { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }

    public class DrinkRepository
    {
        public void AddStore(CoffeeStore cs)
        {
            using (DrinkContext db = new DrinkContext())
            {
                try
                {
                    db.CoffeeStores.Add(cs);
                    db.SaveChanges();
                }
                catch (Exception e)
                { Console.WriteLine(e.ToString()); }
            }
        }

        public void AddDrink(Drink drink)
        {
            using (DrinkContext db = new DrinkContext())
            {
                try
                {
                    db.Drinks.Add(drink);
                    db.SaveChanges();
                }
                catch (Exception e)
                { Console.WriteLine(e.ToString()); }
            }
        }

        public void AddReview(Review review)
        {
            using (DrinkContext db = new DrinkContext())
            {
                try
                {
                    db.Reviews.Add(review);
                    db.SaveChanges();
                }
                catch (Exception e)
                { Console.WriteLine(e.ToString()); }
            }
        }
    }
    //public class ReviewRepository
    //{
    //    public void DoReviewsQuery()
    //    {
    //        using (DrinkContext db = new DrinkContext())
    //        {
    //            var coffeeStoreReviews = db.CoffeeStores.Select(a => new { storeName = a.StoreName, location = a.Location, reviews = a.Reviews });
    //            foreach(var coffeestore in coffeeStoreReviews)
    //            {
    //                var storeReviews = coffeestore.reviews;
    //                foreach(var storeReview in storeReviews)
    //                {
                        

    //                }
    //            }
    //        }
    //    }
  //  }
    public class CodeFirstTest
    {
        static void main()
        {
            DrinkRepository repository = new DrinkRepository();

            //CoffeeStore st1 = new CoffeeStore() { Eircode = "C15C98E", City = City.Limerick, Location = "O' Connell St. Limerick", OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1730, StoreName = "Starbucks", Reviews = new List<Review>() };
            //repository.AddStore(st1);
           // Drink latte = new Drink() { DrinkName = "Cafe Latte", DrinkID = 001, DrinkSize = DrinkSize.grande, Price = 3.40, Eircode = st1.Eircode };
           // Review r1 = new Review() { ReviewID = 999, CustomerName = "mg1", Comment = "The coffee is way too expensive", Rating = Rating.two, Eircode = st1.Eircode };
           // repository.AddDrink(latte);
           // repository.AddReview(r1);

        }
    }
}