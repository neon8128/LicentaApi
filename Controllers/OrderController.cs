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
                Time = DateTime.Now.ToLocalTime().ToString("dd-MM-yyyy h:mm:ss tt"),
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

        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrderById (String OrderId)
        {
            var response = await _repo.GetOrderById(OrderId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("byUser")]
        public async Task<IActionResult> GetOrderByUser(String Email)
        {
            var response = await _repo.GetAllByUser(Email);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
