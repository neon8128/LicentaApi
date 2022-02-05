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
        public String Email { get; set; }
        public String RestaurantId { get; set; }
        public List<ProductOrderDto> Items { get; set; }
        public double TotalPrice { get; set; }
        public Int32 TotalQty { get; set; }
        public String Time { get; set; }
        public String Status { get; set; } = "Not accepted yet";
    }
}
