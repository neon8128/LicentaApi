using LicentaApi.Models;
using System;
using System.Threading.Tasks;

namespace LicentaApi.Repositories.MenuRepository
{
    public interface IMenuRepository
    {
        Task<ServiceResponse<String>> CreateMenu(String RestaurantId, MenuModel Menu);
        Task<ServiceResponse<String>> UpdateMenu(String RestaurantId, MenuModel Menu);
        Task<ServiceResponse<String>> DeleteMenu(String RestaurantId);
        Task<ServiceResponse<String>> GetMenu (String RestaurantId);
    }
}
