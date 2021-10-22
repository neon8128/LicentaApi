using System;

namespace LicentaApi.Models
{
     public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Succes { get; set; } = true;
        public String Message { get; set; } = null;

    }
}