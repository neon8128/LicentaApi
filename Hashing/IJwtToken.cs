using System;
using LicentaApi.Models;

namespace LicentaApi.Hashing
{
    public interface IJwtToken
    {
        String CreateToken(UserModel User);
    }
}