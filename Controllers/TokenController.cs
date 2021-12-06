using System;
using System.Threading.Tasks;
using LicentaApi.Hashing;
using System.Linq;


using Microsoft.AspNetCore.Mvc;

namespace LicentaApi.Controllers
{
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
       


        [HttpGet]
        [Route("Get")]
        public IActionResult GetToken()
        {
            var accessToken = Request.Cookies["jwt"]; //get token from client

            if (!String.IsNullOrEmpty(accessToken))
            {
               //var verified = _tokenService.VerifyToken(accessToken);  //verify it and return jwt token    
                return Ok(accessToken);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}