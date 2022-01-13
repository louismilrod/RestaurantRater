using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class RestaurantListItem
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public double AverageRating { get; set; }

    }
}