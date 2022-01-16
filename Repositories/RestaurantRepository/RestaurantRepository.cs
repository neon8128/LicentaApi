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

namespace LicentaApi.Repositories.RestaurantRepository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private IMongoCollection<RestaurantModel> _restaurants;
        private readonly IWebHostEnvironment _hostEnvironment;
       // private readonly HttpContextAccessor _httpContextAccessor;

        public RestaurantRepository(IDbContext DataContext, IWebHostEnvironment HostEnvironment)
        {
            _restaurants = DataContext.GetRestaurantCollection();
            _hostEnvironment = HostEnvironment;
            //_httpContextAccessor = HttpContextAccessor;
        }
        public async Task<ServiceResponse<string>> AddResturant(RestaurantModel Restaurant)
        {
            var response = new ServiceResponse<String>();

            if(await IsRestaurantDuplicate(Restaurant.Name))
            {
                response.Success = false;
                response.Errors.Add("Restaurant already exists!");
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

        public Task<ServiceResponse<string>> UpdateResturant(string Id, RestaurantModel Restaurant)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsRestaurantDuplicate(String Name) => await _restaurants.AsQueryable()
                .AnyAsync(x => x.Name == Name.ToLower());  // checks for duplicates in the restaurant table 

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
