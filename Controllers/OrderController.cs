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
        public async Task<IActionResult> Create (OrderModel Order)
        {
            var response = await _repo.CreateOrder(Order);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
