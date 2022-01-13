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
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> CreateRating ([FromBody] Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ratings.Add(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRatings()
        {
            /*List<Rating> ratings = await _context.Ratings.ToListAsync();
            return Ok(ratings); --- this is the same as the code below ---*/

            return Ok(await _context.Ratings.ToListAsync());

        }

        [HttpGet]
        [Route("api/Rating/ByRestaurant/{id}")]
        // api/Rating/ByRestaurant/4
        public async Task<IHttpActionResult> GetRatingsForRestaurant(int restaurantId)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            List<Rating> ratings = await _context.Ratings.Where(r => r.Id == restaurantId).ToListAsync();
            return Ok(ratings);
        }

        [HttpGet]
        [Route("api/Rating/ById/{id}")]
        // api/Rating/ByRestaurant/1
        public async Task<IHttpActionResult> GetRatingById(int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }

        
        

        /* [HttpGet]
        public async Task<IHttpActionResult> UpdateRatings([FromUri] int id, [FromBody] Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rating rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            rating.CleanlinessScore = model.CleanlinessScore;
            rating.FoodScore = model.FoodScore;
            rating.AtmosphereScore = model.AtmosphereScore;

            await _context.SaveChangesAsync();
            return Ok("updated!");
        }
        */

        /* [HttpDelete]
        public async Task<IHttpActionResult> DeleteRating([FromUri] int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("Deleted!");
            }

            return InternalServerError();

        }
        */

       
        
    }
}
