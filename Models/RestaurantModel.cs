using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LicentaApi.Models
{
    public class RestaurantModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public String Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        public String ImageName { get; set; }
        
        public String ImagePath { get; set; }
        [BsonIgnore]
        public IFormFile ImageFile { get; set; }
    }
}
