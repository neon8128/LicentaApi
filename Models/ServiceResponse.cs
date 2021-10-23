using System;
using System.Collections.Generic;

namespace LicentaApi.Models
{
     public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public String Message { get; set; } = null;
         public List<String> Errors { get; set; } = new List<String>();

    }
}