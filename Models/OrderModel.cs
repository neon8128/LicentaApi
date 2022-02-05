using LicentaApi.DTO;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LicentaApi.Models
{
    public class OrderModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public String Id { get; set; }
        public String UserID { get; set; }
        public String RestaurantId { get; set; }
        public List<ProductOrderDto> Items { get; set; }
        public String Status { get; set; } = "Not accepted yet";
    }
}
