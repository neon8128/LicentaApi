using MongoDB.Bson.Serialization.Attributes;

namespace LicentaApi.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id {get;set;}
        public string Username {get;set;}
        public string Password {get;set;}
        public string Email {get;set;}
    }
}