using System;

namespace LicentaApi.DTO
{
    public class ProductOrderDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Int32 Quantity { get; set; }
        public String ImagePath { get; set; }
    }
}
