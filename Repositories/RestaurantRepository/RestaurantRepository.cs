using LicentaApi.Data;
using LicentaApi.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using System.Collections.Generic;


namespace LicentaApi.Repositories.RestaurantRepository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private IMongoCollection<RestaurantModel> _restaurants;


        public RestaurantRepository(IDbContext DataContext)
        {
            _restaurants = DataContext.GetRestaurantCollection();

        }
        public async Task<ServiceResponse<string>> AddResturant(RestaurantModel Restaurant)
        {
            var response = new ServiceResponse<String>();

            if(await IsRestaurantDuplicate(Restaurant.Name))
            {
                response.Success = false;
                response.Errors.Add("Restaurant already exists!");
                return response;
            }

            try
            {
                await _restaurants.InsertOneAsync(Restaurant);
                response.Message = "Restaurant was created!";
                response.Success = true;

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse<List<RestaurantModel>>> GetAll()
        {
            var response = new ServiceResponse<List<RestaurantModel>>();
            try
            {
                var restaurants = await _restaurants.AsQueryable().ToListAsync();
                if(restaurants != null)
                {
                    response.Data = restaurants;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "There are no restaurants!";
                }
                
            }
            catch (Exception ex)
            {
                response.Success=false;
                response.Message = ex.ToString();

            }
            return response;
        }

        public Task<ServiceResponse<string>> GetByName(string Name)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<string>> UpdateResturant(string Id, RestaurantModel Restaurant)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsRestaurantDuplicate(String Name) => await _restaurants.AsQueryable()
                .AnyAsync(x => x.Name == Name.ToLower());  // checks for duplicates in the restaurant table 

        
    }
}
