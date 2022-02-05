using LicentaApi.DTO.Order;
using LicentaApi.Models;
using LicentaApi.Repositories.OrderRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                UserID = Order.UserID,
                RestaurantId = Order.RestaurantId,
                Items = Order.Items,
                Status = Order.Status
            };
            var response = await _repo.CreateOrder(temp);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
