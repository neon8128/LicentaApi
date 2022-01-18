using System.Threading.Tasks;
using LicentaApi.DTO;
using LicentaApi.Models;
using LicentaApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto LoginUser)
        {
            var response = await _auth.Login(LoginUser.Email, LoginUser.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                HttpContext.Response.Cookies.Append("jwt", response.Data, new CookieOptions
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    IsEssential = true

                }) ;

                return Ok(response);
            }
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

            if (!response.Success)
            {

                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

       
        [HttpPost("LogOut")]
        public IActionResult LogOut()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return Ok();
        }

    }
}