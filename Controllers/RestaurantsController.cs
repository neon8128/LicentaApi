    using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantsController: ControllerBase
    {
        public RestaurantsController()
        {
            
        }
        
        [HttpGet("GetAll")]
        [Authorize]
        public  IActionResult GetAll(string Id)
        {
            var response = "it works!";
            return Ok(response);
        }
    }
}