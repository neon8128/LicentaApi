using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LicentaApi.DTO;
using LicentaApi.Models;
using LicentaApi.Repositories.RestaurantRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRestaurant([FromForm]RestaurantDTO Restaurant)
        {
            var temp = new RestaurantModel
            {
                Name = Restaurant.Name,
                Address = Restaurant.Address,
                ImageFile = Restaurant.ImageFile
            };
     
           var response = await _restaurantRepo.AddResturant(temp);
                
            if (!response.Success)
            {
               return BadRequest(response);
            }
            return Ok(response);
        }


        
    }
}