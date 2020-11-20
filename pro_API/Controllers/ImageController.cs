using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pro_API.Repositories;
using pro_Models.Models;
using pro_Models.ViewModels;

namespace pro_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<ImageVM>>> Search(string name)
        {
            try
            {
                var result = await imageRepository.Search(name);

                if (result.Any())
                {
                    return result;
                }

                return NotFound();
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetImages()
        {
            try
            {
                return Ok(await imageRepository.GetImages());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ImageVM>> GetImage(int id)
        {
            try
            {
                var result = await imageRepository.GetImage(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ImageVM>> CreateImage(ImageVM imageVM)
        {
            try
            {
                if (imageVM == null) return BadRequest();

                await imageRepository.CreateImage(imageVM);

                return CreatedAtAction(nameof(GetImage),
                    new { id = imageVM.Image.Id }, imageVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ImageVM>> UpdateImage(int id, ImageVM imageVM)
        {
            try
            {
                if (id != imageVM.Image.Id)
                    return BadRequest("Image ID mismatch");

                var imageToUpdate = await imageRepository.GetImage(id);

                if (imageToUpdate == null)
                    return NotFound($"Image with Id = {id} not found");

                await imageRepository.UpdateImage(imageVM);

                return CreatedAtAction(nameof(GetImage), new { id = imageVM.Image.Id }, imageVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ImageVM>> DeleteImage(int id)
        {
            try
            {
                var imageToDelete = await imageRepository.GetImage(id);

                if (imageToDelete == null)
                {
                    return NotFound($"Image with Id = {id} not found");
                }

                return await imageRepository.DeleteImage(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
