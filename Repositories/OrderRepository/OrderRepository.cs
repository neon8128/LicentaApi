using LicentaApi.Data;
using LicentaApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicentaApi.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private IMongoCollection<OrderModel> _order;

        public OrderRepository(IDbContext DataContext)
        {
           _order = DataContext.GetOrderCollection();
        }
        public async Task<ServiceResponse<String>> CreateOrder(OrderModel Model)
        {
            var response = new ServiceResponse<string>();
            try
            {
               
                await _order.InsertOneAsync(Model);
                response.Message = "Order was created!";
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse<List<OrderModel>>> GetAllByRestaurant(string RestaurantId)
        {
            var response = new ServiceResponse<List<OrderModel>> ();
            try
            {

                var filter = Builders<OrderModel>.Filter.Eq("Restaurant_Id", RestaurantId);
                var items = await _order.Find(filter).ToListAsync();
                if (items.Any())
                {
                    response.Data = items;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "There are no items!";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }
            return response;
        }

        public Task<ServiceResponse<List<OrderModel>>> GetAllByUser(string Email)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<List<OrderModel>>> GetOrderById(string OrderId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<OrderModel>> UpdateOrder(OrderModel Model)
        {
            throw new System.NotImplementedException();
        }
    }
}
