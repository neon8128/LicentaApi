using LicentaApi.DTO;
using LicentaApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicentaApi.Repositories.RestaurantRepository
{
    public interface IRestaurantRepository
    {
        Task<ServiceResponse<List<RestaurantModel>>> GetAll();
        Task<ServiceResponse<String>> GetByName(String Name);
        Task<ServiceResponse<String>> AddResturant(RestaurantModel Restaurant);
        Task<ServiceResponse<String>> UpdateResturant(String Id, RestaurantModel Restaurant);
        Task<ServiceResponse<RestaurantModel>> GetByUserName();
    }
}
