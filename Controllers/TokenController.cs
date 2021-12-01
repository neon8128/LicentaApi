using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Mvc;

namespace LicentaApi.Controllers
{
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetToken()
        {
            var accessToken =  await HttpContext.GetTokenAsync("token");          
            if (!String.IsNullOrEmpty(accessToken))
            {
                return Ok(accessToken);
            }
            else
            {
              return BadRequest();
            }
        }
    }
}