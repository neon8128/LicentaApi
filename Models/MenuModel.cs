using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LicentaApi.Models
{
    public class MenuModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public String Id { get; set; }
        public String Restaurant_Id { get; set; }
        public String Item { get; set; }
        public String Description { get; set; }
        public Double Price { get; set; }
        public List<String> Categories { get; set; }
        public String ImageName { get; set; }

        public String ImagePath { get; set; }
        [BsonIgnore]
        public IFormFile ImageFile { get; set; }
    }
}
