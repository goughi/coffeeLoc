using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffee.Models;

namespace coffee.ViewModels
{
    public class CoffeeIndexData
    {
        public IEnumerable<CoffeeStore> coffeeStores { get; set; }
        public IEnumerable<Review> reviews { get; set; }
        public IEnumerable<Drink> drinks { get; set; }
    }
}