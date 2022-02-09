using LicentaApi.Data;
using LicentaApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace LicentaApi.Repositories.MenuRepository
{
    public class MenuRepository : IMenuRepository
    {
        private IMongoCollection<MenuModel> _menu;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MenuRepository(IDbContext DataContext, IWebHostEnvironment HostEnvironment)
        {
            _menu = DataContext.GetMenuCollection();
            _hostEnvironment = HostEnvironment;
        }
    
        public async Task<ServiceResponse<string>> Create( MenuModel Menu)
        {
            
            var response = new ServiceResponse<string>();
            try
            {
                Menu.ImageName = await SaveImage(Menu.ImageFile);
                Menu.ImagePath = String.Format("{0}://{1}{2}/Images/{3}", "https", "localhost:", "44321", Menu.ImageName);
                await _menu.InsertOneAsync(Menu);
                response.Message = "Menu was created!";
                response.Success = true;
            }
            catch(Exception e)
            {
                response.Success = false;
                response.Data = e.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse<string>> Delete(string ItemId)
        {
            var response = new ServiceResponse<String>();
            try
            {
                await _menu.DeleteOneAsync(x =>x.Id == ItemId);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Data = e.ToString();
            }
            return response;
        }
        
        public async Task<ServiceResponse<List<MenuModel>>> GetAll(string RestaurantId)
        {
            var response = new ServiceResponse<List<MenuModel>>();
            try
            {
                var filter = Builders<MenuModel>.Filter.Eq("Restaurant_Id", RestaurantId);
                var items = await _menu.Find(x => x.Restaurant_Id == RestaurantId).ToListAsync();
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
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();

            }
            return response;
        }


        public async Task<ServiceResponse<string>> Update(string ProductId, MenuModel Menu)
        {
            var response = new ServiceResponse<String>();
            ReplaceOneResult replace = null;
            UpdateResult updateResult = null;
            try
            {
                if (Menu.ImageFile != null)
                {
                    //DeleteImage(Menu.ImageName);
                    Menu.ImageName = await SaveImage(Menu.ImageFile);
                    Menu.ImagePath = String.Format("{0}://{1}{2}/Images/{3}", "https", "localhost:", "44321", Menu.ImageName);
                    replace =  await _menu.ReplaceOneAsync(x => x.Id == Menu.Id, Menu);
                }
                else
                {
                    var updateDefinition = Builders<MenuModel>.Update
                        .Set(a => a.Restaurant_Id, Menu.Restaurant_Id)
                        .Set(a => a.Item, Menu.Item)
                        .Set(a => a.Description, Menu.Description)
                        .Set(a => a.Price, Menu.Price)
                        .Set(a => a.Categories, Menu.Categories);
                    updateResult = await _menu.UpdateOneAsync(x => x.Id == Menu.Id,updateDefinition);
                }

                
                if(replace != null || updateResult!=null)
                {
                    response.Success = true;
                    response.Message = "updated";
                }
                else
                {
                    response.Success = false;
                }
                
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            return response;
        }

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
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
