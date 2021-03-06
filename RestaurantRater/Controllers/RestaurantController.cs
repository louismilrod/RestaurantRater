using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext(); //like making a list in gold badge to do crud on
        
        //POST (create)
        // api/Restasurant
        [HttpPost]   //saying its a post
        public async Task<IHttpActionResult> CreateRestaurant ([FromBody] Restaurant model) 
        {
            /* if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            //if the model is valid
            if (ModelState.IsValid)
            {
                Store the model in the database
                _context.Restaurants.Add(model);
                int changeCount = await _context.SaveChangesAsync();

                return Ok("Your restaurant was created!");
            }

            //The model is not valid, go ahead and reject it
            return BadRequest(ModelState); */

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Restaurants.Add(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet] //Get method, async task so it can run while other code is running
        public async Task<IHttpActionResult> GetRestaurant()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();

            List<RestaurantListItem> restaurantLists = restaurants.Select(r => new RestaurantListItem()
            {
                Name = r.Name,
                Location = r.Address,
                AverageRating = r.Ratings.Count == 0 ? 0 : r.Ratings.Select(rating => rating.AvergeRating).Average()
            }).ToList();

            return Ok(restaurantLists);

           /* List<RestaurantListItem> restaurentListTwo = restaurants.Select(r =>
            {
                List<Ratings> ratings = r.Ratings;
                double average;
                if (ratings.Count == 0)                         this is the same as the few lines above.
                {
                    average = 0;
                }
                average = ratings.Select(a => a.AvergeRating).Average();

                return new RestaurantListItem()
                {
                    Name = r.Name,
                    Location = r.Address,
                    AverageRating = average
                };
            }).ToList();*/
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);  //creating a restaurant variable that gets grabbed by id 

            if (restaurant == null)  //if there is no restaurant return not found method
            {
                return NotFound();
            }

            return Ok(restaurant);
        }


        [HttpPut]  //updating a restaurant kind of like a post but it targets a specific object that exists
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id,[FromBody] Restaurant model)  
            //[FromUri] is to indicate that the id is coming from the address, then we need the new restaurant data from the body of the request
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Restaurant restaurant = await _context.Restaurants.FindAsync(id); //just like we did before, find the restaurant by Id

            if (restaurant == null)
            {
                return NotFound();
            }

            restaurant.Name = model.Name;
            restaurant.Address = model.Address;
            
            await _context.SaveChangesAsync();
            return Ok("updated!");

            /*int updateCount = await _context.SaveChangesAsync();
            if (updateCount == 1)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            } */
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok();
            }

            return InternalServerError();

        }

    }
}
