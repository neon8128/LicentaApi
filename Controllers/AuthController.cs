using System.Threading.Tasks;
using LicentaApi.DTO;
using LicentaApi.Models;
using LicentaApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LicentaApi.Controllers
{
     [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _auth;
        public AuthController(IAuthRepository Auth)
        {
            _auth = Auth;
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto RegisterUserDto)
        {
           // var validator = new RegisterUserValidator();
            //var results = validator.Validate(RegisterUserDto);

           
            var response = await _auth.Register(
                new UserModel
                {
                    Username = RegisterUserDto.Username,
                    Email = RegisterUserDto.Email
                },
                RegisterUserDto.Password
            );
            //var response = await _auth.Register(RegisterUserDto);

            if ( /*!results.IsValid */ !response.Success)
            {
               
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}