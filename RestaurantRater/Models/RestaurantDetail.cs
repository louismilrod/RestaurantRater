using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class RestaurantDetail
    {
        //use RatingList Items instead of actual Ratings
        // Avoid the recursive problem
        public List<RatingListItem> Ratings { get; set; }
    }
}