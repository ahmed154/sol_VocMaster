using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IImageService
    {
        Task<List<ImageVM>> GetImages();
        Task<ImageVM> GetImage(int id);
        Task<ImageVM> UpdateImage(int id, ImageVM imageVM);
        Task<ImageVM> CreateImage(ImageVM imageVM);
        Task<ImageVM> DeleteImage(int id);
    }
}
