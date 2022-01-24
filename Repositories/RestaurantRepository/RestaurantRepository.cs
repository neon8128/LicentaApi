using LicentaApi.Data;
using LicentaApi.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace LicentaApi.Repositories.RestaurantRepository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private IMongoCollection<RestaurantModel> _restaurants;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContext;

        public RestaurantRepository(IDbContext DataContext, IWebHostEnvironment HostEnvironment, IHttpContextAccessor HttpContext)
        {
            _restaurants = DataContext.GetRestaurantCollection();
            _hostEnvironment = HostEnvironment;
            _httpContext = HttpContext;

        }
        private String GetUserName() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        public async Task<ServiceResponse<string>> AddResturant(RestaurantModel Restaurant)
        {
            var response = new ServiceResponse<String>();

            if(await IsRestaurantDuplicate(Restaurant.Name))
            {
                response.Success = false;
                response.Errors.Add("Restaurant already exists!");
                return response;
            }
            if(await IsManagerDuplicate(Restaurant.UserManager))
            {
                response.Success = false;
                response.Errors.Add("Cannot manage more than one restaurant!");
                return response;
            }

            try
            {
                Restaurant.ImageName = await SaveImage(Restaurant.ImageFile);
                Restaurant.ImagePath = String.Format("{0}://{1}{2}/Images/{3}", "https", "localhost:", "44321", Restaurant.ImageName);
                await _restaurants.InsertOneAsync(Restaurant);
                response.Message = "Restaurant was created!";
                response.Success = true;

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse<List<RestaurantModel>>> GetAll()
        {
            var response = new ServiceResponse<List<RestaurantModel>>();
            try
            {
                var restaurants = await _restaurants.AsQueryable().ToListAsync();
                if(restaurants != null)
                {
                    response.Data = restaurants;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "There are no restaurants!";
                }
                
            }
            catch (Exception ex)
            {
                response.Success=false;
                response.Message = ex.ToString();

            }
            return response;
        }

        public Task<ServiceResponse<string>> GetByName(string Name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<RestaurantModel>> GetByUserName(String Name)
        {
            var response = new ServiceResponse<RestaurantModel>();
            if (String.IsNullOrEmpty(Name))
            {
                response.Data=null;
                response.Success = false;
                response.Message = "Name is empty!";
                return response;
            }
  
            try
            {
                var restaurant = await _restaurants.AsQueryable().FirstAsync(x => x.UserManager == Name);

                if (restaurant != null)
                {
                    response.Data = restaurant;
                    response.Success = true;
                }
                else { response.Success = false; }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            return response;

        }

        public Task<ServiceResponse<string>> UpdateResturant(string Id, RestaurantModel Restaurant)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsRestaurantDuplicate(String Name) => await _restaurants.AsQueryable()
                .AnyAsync(x => x.Name == Name.ToLower());  // checks for duplicates in the restaurant table 

        public async Task<bool> IsManagerDuplicate(String UserManager) => await _restaurants.AsQueryable()
                .AnyAsync(x => x.UserManager == UserManager.ToLower());  // checks for dupolicates in manager 

        //saves image and return image name
        public async Task<String> SaveImage(IFormFile ImageFile)
        {
            string imageName = new String(ImageFile.FileName.Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(ImageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        
    }
}
