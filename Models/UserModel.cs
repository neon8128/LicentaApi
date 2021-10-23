using MongoDB.Bson.Serialization.Attributes;

namespace LicentaApi.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id {get;set;}
        public string Username {get;set;}
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
                
        public string Role { get; set; }= "User";
        public string Email {get;set;}
    }
}