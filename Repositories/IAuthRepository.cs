using System;
using System.Threading.Tasks;
using LicentaApi.Models;

namespace LicentaApi.Repositories
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<String>> Login(String Username);

        Task<ServiceResponse<String>> Register(UserModel User);

        Task UserExists (String Username);
    }
}