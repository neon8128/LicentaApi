using System;
using System.Collections.Generic;

namespace LicentaApi.DTO.Order
{
    public class OrderDto
    {
        public String UserID { get; set; }
        public String RestaurantId { get; set; }
        public List<ProductOrderDto> Items { get; set; }
        public String Status { get; set; } = "Not accepted yet";
    }
}
