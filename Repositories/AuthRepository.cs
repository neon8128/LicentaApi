using System;
using System.Threading.Tasks;
using AutoMapper;
using LicentaApi.Data;
using LicentaApi.DTO;
using LicentaApi.Hashing;
using LicentaApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LicentaApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private  IMongoCollection<UserModel> _users;
        private readonly IMapper _mapper;
        private readonly IJwtToken _jwtToekn;

        public AuthRepository(IDbContext DataContext,IMapper Mapper, IJwtToken JwtToekn)
        {
            _users = DataContext.GetUserCollection();
            _mapper = Mapper;
            _jwtToekn = JwtToekn;
        }
        public async Task<ServiceResponse<string>> Login(string Username, String Password)
        {
             var response = new ServiceResponse<String>();
  
           var user = await _users.AsQueryable().FirstOrDefaultAsync(x =>x.Username == Username);
            var hashingObject = new HashingAlgorithms();
            
            if (user == null)
            {
                response.Success = false;
                response.Errors.Add("User not found");
            }
            else if (!hashingObject.VerifyHash(Password, user.PasswordHash, user.Salt))
            {
                response.Success = false;
                response.Errors.Add("Wrong Password");
            }
            else
            {
                response.Data = _jwtToekn.CreateToken(user);
                response.Success = true;
                response.Message = "You have successfully loged in!";

            }
            return response;
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