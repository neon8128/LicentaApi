using LicentaApi.Models;
using MongoDB.Driver;

namespace LicentaApi.Data
{
    public interface IDbContext
    {
        IMongoCollection<UserModel> GetUserCollection();
        IMongoCollection<RestaurantModel> GetRestaurantCollection();
        IMongoCollection<MenuModel> GetMenuCollection();

    }
}