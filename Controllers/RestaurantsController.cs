    using System.Threading.Tasks;
using LicentaApi.DTO;
using LicentaApi.Models;
using LicentaApi.Repositories.RestaurantRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantsController: ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepo;

        public RestaurantsController(IRestaurantRepository RestaurantRepo)
        {
            _restaurantRepo = RestaurantRepo;
        }
        
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
           return Ok( await _restaurantRepo.GetAll());
        }
        [HttpPost("CreateRestaurant")]
        public async Task<IActionResult> CreateRestaurant(RestaurantDTO Restaurant)
        {
            var response = await _restaurantRepo.AddResturant(
                new RestaurantModel
                {
                    Name = Restaurant.Name,
                    Address = Restaurant.Address,
                });
            if (!response.Success)
            {
               return BadRequest(response);
            }
            return Ok(response);
        }
    }
}