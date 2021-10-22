using LicentaApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LicentaApi.Data
{
    public class DbContext : IDbContext
    {
        private readonly IMongoCollection<UserModel> _users; 
        public DbContext(IOptions<DbConfig> DbConfig)
        {
            var client = new MongoClient(DbConfig.Value.ConnectionString);
            var database = client.GetDatabase(DbConfig.Value.DatabaseName);
            _users = database.GetCollection<UserModel>("User");

        }
        public IMongoCollection<UserModel> GetUserCollection() => _users;

    }
}