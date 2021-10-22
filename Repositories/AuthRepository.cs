using System;
using System.Threading.Tasks;
using LicentaApi.Data;
using LicentaApi.Models;
using MongoDB.Driver;

namespace LicentaApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private  IMongoCollection<UserModel> _users;
        public AuthRepository(IDbContext DataContext)
        {
            _users = DataContext.GetUserCollection();
        }
        public Task<ServiceResponse<string>> Login(string Username)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<string>> Register(UserModel User)
        {
            var response = new ServiceResponse<String>();
            try
            {
               await  _users.InsertOneAsync(User);
               response.Succes = true;
               response.Message = "User was created";

            }
            catch(Exception ex)
            {
                response.Succes = false;
                response.Message = ex.ToString();
            }
          
          return response;
        }

        public Task UserExists(string Username)
        {
            throw new System.NotImplementedException();
        }
    }
}