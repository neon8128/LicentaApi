using System;
using System.IdentityModel.Tokens.Jwt;
using LicentaApi.Models;

namespace LicentaApi.Hashing
{
    public interface IJwtToken
    {
        String CreateToken(UserModel User);

        JwtSecurityToken VerifyToken(String Token);
    }
}