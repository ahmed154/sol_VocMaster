using Microsoft.EntityFrameworkCore;
using pro_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pro_API.Repositories;
using pro_Models.Models;
using pro_Models.ViewModels;
using AutoMapper;

namespace pro_API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public ImageRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<ImageVM>> IImageRepository.Search(string name)
        {
            List<ImageVM> imageVMs = new List<ImageVM>();

            IQueryable<Image> query = appDbContext.Images;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Comment.Contains(name));
            }

            var images = await query.ToListAsync();

            foreach (var image in images)
            {
                imageVMs.Add(new ImageVM { Image = image });
            }
            return imageVMs;
        }
        public async Task<List<ImageVM>> GetImages()
        {
            List<ImageVM> imageVMs = new List<ImageVM>();
            var images = await appDbContext.Images.ToListAsync();
            foreach (var image in images)
            {
                imageVMs.Add(new ImageVM { Image = image});
            }
            return imageVMs; 
        }
        public async Task<ImageVM> GetImage(int id)
        {
            ImageVM imageVM = new ImageVM();
            imageVM.Image = await appDbContext.Images.FirstOrDefaultAsync(e => e.Id == id);
            return imageVM;
        }
        public async Task<ImageVM> CreateImage(ImageVM imageVM)
        {
            var result = await appDbContext.Images.AddAsync(imageVM.Image);

            await appDbContext.SaveChangesAsync();

            imageVM.Image = result.Entity;
            return imageVM;
        }
        public async Task<ImageVM> UpdateImage(ImageVM imageVM)
        {
            Image result = await appDbContext.Images
                .FirstOrDefaultAsync(e => e.Id == imageVM.Image.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(imageVM.Image, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new ImageVM { Image = result };
            }

            return null;
        }
        public async Task<ImageVM> DeleteImage(int imageId)
        {
            var result = await appDbContext.Images
                .FirstOrDefaultAsync(e => e.Id == imageId);
            if (result != null)
            {
                appDbContext.Images.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new ImageVM { Image = result };
            }

            return null;
        }

        public Task<Image> GetImageByname(Image image)
        {
            throw new NotImplementedException();
        }
    }
}
