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
    public class JwtToken : IJwtToken
    {
        private readonly IConfiguration _configuration;
        public JwtToken(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public String CreateToken(UserModel User)
        //Creates Token
        {

            var claims = new List<Claim> //list of claims in which we store username, email and role
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
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public JwtSecurityToken VerifyToken(String Token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["jwt_secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch(Exception e)
            {
                
            }
            return null;

        }
    }
}