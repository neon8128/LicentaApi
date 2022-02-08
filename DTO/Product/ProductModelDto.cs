using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace LicentaApi.Models
{
    public class ProductModelDto
    {
        public String Item { get; set; }
        public Double Price { get; set; }
        public String RestaurantID { get; set; }
        public String Description { get; set; }
        public List<String> Categories { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
