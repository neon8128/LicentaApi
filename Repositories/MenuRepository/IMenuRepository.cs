using LicentaApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicentaApi.Repositories.MenuRepository
{
    public interface IMenuRepository
    {
        Task<ServiceResponse<String>> Create( MenuModel Menu);
        Task<ServiceResponse<String>> Update(String RestaurantId, MenuModel Menu);
        Task<ServiceResponse<String>> Delete(String RestaurantId);
        Task<ServiceResponse<List<MenuModel>>> GetAll(String RestaurantId);
    }
}
