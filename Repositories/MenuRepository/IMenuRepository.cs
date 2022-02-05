using LicentaApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicentaApi.Repositories.MenuRepository
{
    public interface IMenuRepository
    {
        Task<ServiceResponse<String>> Create( MenuModel Menu);
        Task<ServiceResponse<String>> Update(String ProductId, MenuModel Menu);
        Task<ServiceResponse<String>> Delete(String ItemId);
        Task<ServiceResponse<List<MenuModel>>> GetAll(String RestaurantId);
    }
}
