using LicentaApi.Models;
using System.Threading.Tasks;

namespace LicentaApi.Repositories.MenuRepository
{
    public class MenuIRepository : IMenuRepository
    {
    
        public Task<ServiceResponse<string>> CreateMenu(string RestaurantId, MenuModel Menu)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<string>> DeleteMenu(string RestaurantId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<string>> GetMenu(string RestaurantId)
        {
            throw new System.NotImplementedException();
        }


        public Task<ServiceResponse<string>> UpdateMenu(string RestaurantId, MenuModel Menu)
        {
            throw new System.NotImplementedException();
        }
    }
}
