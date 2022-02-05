using LicentaApi.Data;
using LicentaApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                var items = await _menu.Find(filter).ToListAsync();
                if (items.Count >0)
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
            try
            {
                if (Menu.ImageFile != null)
                {
                    DeleteImage(Menu.ImageName);
                    Menu.ImageName = await SaveImage(Menu.ImageFile);
                    Menu.ImagePath = String.Format("{0}://{1}{2}/Images/{3}", "https", "localhost:", "44321", Menu.ImageName);
                }

                var temp =  await _menu.ReplaceOneAsync(x=>x.Id == Menu.Id, Menu);
                if(temp != null)
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
