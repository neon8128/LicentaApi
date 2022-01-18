using System;
using System.Threading.Tasks;
using LicentaApi.Models;

namespace LicentaApi.Repositories
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<String>> Login(String Username, String Passowrd);

        Task<ServiceResponse<String>> Register(UserModel User, String Password);

    }
}