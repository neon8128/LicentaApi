using Microsoft.AspNetCore.Http;
using System;

namespace LicentaApi.DTO
{
    public class RestaurantDTO
    {
        public String Name { get; set; }
        public String Address { get; set; }
        public IFormFile ImageFile{ get; set; }
    }
}
