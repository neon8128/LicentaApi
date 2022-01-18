using System;
using System.Threading.Tasks;
using LicentaApi.Hashing;
using System.Linq;


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LicentaApi.Controllers
{
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IJwtToken _tokenService;

        public TokenController(IJwtToken Token)
        {
            _tokenService = Token;
        }
       


        [HttpGet]
        [Route("Get")]
 
        public IActionResult GetToken()
        {
            var accessToken = Request.Cookies["jwt"]; //get token from client

            if (!String.IsNullOrEmpty(accessToken))
            {
               var verified = _tokenService.VerifyToken(accessToken);  //verify it and return jwt token    
                return Ok(verified);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}