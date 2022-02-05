using LicentaApi.DTO.Order;
using LicentaApi.Models;
using LicentaApi.Repositories.OrderRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LicentaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrderController: ControllerBase
    {
        private readonly IOrderRepository _repo;

        public OrderController(IOrderRepository Repo)
        {
            _repo = Repo;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create (OrderDto Order)
        {
            var temp = new OrderModel
            {
                Email = Order.Email,
                RestaurantId = Order.RestaurantId,
                Items = Order.Items,
                Status = Order.Status,
                TotalQty = Order.TotalQty,
                TotalPrice = Order.TotalPrice,
            };
            var response = await _repo.CreateOrder(temp);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("restaurant")]
        public async Task<IActionResult> GetByRestaurant (String RestaurantId)
        {
            var response = await _repo.GetAllByRestaurant(RestaurantId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
