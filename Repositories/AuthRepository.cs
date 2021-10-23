using System;
using System.Threading.Tasks;
using AutoMapper;
using LicentaApi.Data;
using LicentaApi.DTO;
using LicentaApi.Hashing;
using LicentaApi.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LicentaApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private  IMongoCollection<UserModel> _users;
        private readonly IMapper _mapper;

        public AuthRepository(IDbContext DataContext,IMapper Mapper)
        {
            _users = DataContext.GetUserCollection();
            _mapper = Mapper;
        }
        public Task<ServiceResponse<string>> Login(string Username)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<String>> Register(UserModel User, String Password)
        {   
            var hashingObject = new HashingAlgorithms();
            var response = new ServiceResponse<String>();

             if (await IsUserDuplicateAsync(User.Username))
            {
                response.Success = false;
                response.Errors.Add("Username already taken");
                return response;
            }

            if (await IsEmailDuplicateAsync(User.Email))
            {
                response.Success = false;
                response.Errors.Add("Email is already in use");
                return response;
            }

            hashingObject.CreateHash(Password, out byte[] hash, out byte[] salt);
            User.PasswordHash = hash;
            User.Salt = salt;
            try
            {
               await  _users.InsertOneAsync(User);
               response.Success = true;
               response.Message = "User was created";

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
          
          return response;
        }

     public async Task<bool> IsUserDuplicateAsync(String Username) => await _users.AsQueryable()
                .AnyAsync(x => x.Username.ToLower() == Username.ToLower());  // checks for duplicates in the user table 

    public async Task<bool> IsEmailDuplicateAsync(String Email) => await _users.AsQueryable()
                .AnyAsync(x => x.Email.ToLower() == Email.ToLower()); //Checks for duplicates in Email 

    }
}