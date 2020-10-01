using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IImageRepository
    {
        Task<List<ImageVM>> Search(string name);
        Task<List<ImageVM>> GetImages();
        Task<ImageVM> GetImage(int imageId);
        Task<ImageVM> CreateImage(ImageVM imageVM);
        Task<ImageVM> UpdateImage(ImageVM image);
        Task<ImageVM> DeleteImage(int imageId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Image> GetImageByName(string name);
        Task<Image> GetImageByname(Image image);
    }
}
