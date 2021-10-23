using AutoMapper;
using LicentaApi.DTO;
using LicentaApi.Models;

namespace LicentaApi
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserDto,UserModel>();
        }
    }
}