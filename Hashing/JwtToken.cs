using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LicentaApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LicentaApi.Hashing
{
    public class JwtToken:IJwtToken
    {
        private readonly IConfiguration _configuration;
        public JwtToken(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public String CreateToken(UserModel User)         
           //Creates Token
        {
            
            var claims = new List<Claim> //list of claims in which we user username and email
            {
                new Claim(ClaimTypes.Name,User.Username),
                new Claim(ClaimTypes.Email,User.Email),
                new Claim(ClaimTypes.Role,User.Role)

            };
            //creates a key based on a string 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt_secret"]));

            //encoding  using sha512 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor //creating a tokendescriptor based on claims
                                                                                  //and an expiry date
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}