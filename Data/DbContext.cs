using LicentaApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LicentaApi.Data
{
    public class DbContext : IDbContext
    {
        private readonly IMongoCollection<UserModel> _users;
        private readonly IMongoCollection<RestaurantModel> _restaurants;
        private readonly IMongoCollection<MenuModel> _menus;
        private readonly IMongoCollection<OrderModel> _orders;

        public DbContext(IOptions<DbConfig> DbConfig)
        {
            var client = new MongoClient(DbConfig.Value.ConnectionString);
            var database = client.GetDatabase(DbConfig.Value.DatabaseName);
            _users = database.GetCollection<UserModel>("User");
            _restaurants = database.GetCollection<RestaurantModel>("Restaurant");
            _menus = database.GetCollection<MenuModel>("Menu");
            _orders = database.GetCollection<OrderModel>("Order");
        }
        public IMongoCollection<UserModel> GetUserCollection() => _users;
        public IMongoCollection<RestaurantModel> GetRestaurantCollection() => _restaurants;
        public IMongoCollection<MenuModel> GetMenuCollection() => _menus;
        public IMongoCollection<OrderModel> GetOrderCollection() => _orders;

    }
}