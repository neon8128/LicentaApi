using LicentaApi.Models;
using LicentaApi.Repositories.MenuRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LicentaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductsController:ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public ProductsController(IMenuRepository MenuRepository)
        {
            _menuRepository = MenuRepository;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] ProductModelDto Product)
        {
            var temp = new MenuModel
            {
                Restaurant_Id = Product.RestaurantID,
                Item = Product.Item,
                Price = Product.Price,
                Categories = Product.Categories,
                Description = Product.Description,
                ImageFile = Product.ImageFile,
            };
            var response = await _menuRepository.Create(temp);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("Delete/{id}")]
        public async Task <IActionResult> Delete(string Id)
        {
            var response = await _menuRepository.Delete(Id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(string Id)
        {
            var response = await _menuRepository.GetAll(Id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string Id, [FromForm] ProductModelDto Product)
        {
            var temp = new MenuModel
            {
                Id = Id,
                Restaurant_Id = Product.RestaurantID,
                Item = Product.Item,
                Price = Product.Price,
                Categories = Product.Categories,
                Description = Product.Description,
                ImageFile = Product.ImageFile,
            };
            var response = await _menuRepository.Update(Id, temp);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
