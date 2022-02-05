using LicentaApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicentaApi.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<ServiceResponse<List<OrderModel>>> GetAllByUser(string UserId);
        Task<ServiceResponse<List<OrderModel>>> GetAllByRestaurant(string RestaurantId);
        Task<ServiceResponse<List<OrderModel>>> GetOrderById(string OrderId);
        Task<ServiceResponse<string>> CreateOrder(OrderModel Model);
        Task<ServiceResponse<OrderModel>> UpdateOrder(OrderModel Model);
    }
}
