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
    public class IdiomController : ControllerBase
    {
        private readonly IIdiomRepository idiomRepository;

        public IdiomController(IIdiomRepository idiomRepository)
        {
            this.idiomRepository = idiomRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<IdiomVM>>> Search(string name)
        {
            try
            {
                var result = await idiomRepository.Search(name);

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
        public async Task<ActionResult> GetIdioms()
        {
            try
            {
                return Ok(await idiomRepository.GetIdioms());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IdiomVM>> GetIdiom(int id)
        {
            try
            {
                var result = await idiomRepository.GetIdiom(id);

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
        public async Task<ActionResult<IdiomVM>> CreateIdiom(IdiomVM idiomVM)
        {
            try
            {
                if (idiomVM == null)return BadRequest();

                // Add custom model validation error
                Idiom idiom = await idiomRepository.GetIdiomByname(idiomVM.Idiom);
                if (idiom != null)
                {
                    ModelState.AddModelError("Name", $"Idiom name: {idiomVM.Idiom.Text} already in use");
                    return BadRequest(ModelState);
                }

                await idiomRepository.CreateIdiom(idiomVM);

                return CreatedAtAction(nameof(GetIdiom),
                    new { id = idiomVM.Idiom.Id }, idiomVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<IdiomVM>> UpdateIdiom(int id, IdiomVM idiomVM)
        {
            try
            {
                if (id != idiomVM.Idiom.Id)
                    return BadRequest("Idiom ID mismatch");

                // Add custom model validation error
                Idiom idiom = await idiomRepository.GetIdiomByname(idiomVM.Idiom);
                if (idiom != null)
                {
                    ModelState.AddModelError("Name", $"Idiom name: {idiomVM.Idiom.Text} already in use");
                    return BadRequest(ModelState);
                }

                var idiomToUpdate = await idiomRepository.GetIdiom(id);

                if (idiomToUpdate == null)
                    return NotFound($"Idiom with Id = {id} not found");

                await idiomRepository.UpdateIdiom(idiomVM);

                return CreatedAtAction(nameof(GetIdiom), new { id = idiomVM.Idiom.Id }, idiomVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<IdiomVM>> DeleteIdiom(int id)
        {
            try
            {
                var idiomToDelete = await idiomRepository.GetIdiom(id);

                if (idiomToDelete == null)
                {
                    return NotFound($"Idiom with Id = {id} not found");
                }

                return await idiomRepository.DeleteIdiom(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
