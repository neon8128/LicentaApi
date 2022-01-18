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
        public String Price { get; set; }
        public List<String> Categories { get; set; }
    }
}
