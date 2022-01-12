using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        
        //POST (create)
        // api/Restasurant
        [HttpPost]
        public async Task<IHttpActionResult> CreateRestaurant ([FromBody] Restaurant model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            // if the model is valid
            if (ModelState.IsValid)
            {
                //Store the model in the database
                _context.Restaurant.Add(model);
                int changeCount = await _context.SaveChangesAsync();

                return Ok("Your restaurant was created!");
            }

            // The model is not valid, go ahead and reject it
            return BadRequest(ModelState);
        }
    }
}
