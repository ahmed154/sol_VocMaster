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
    public class LangController : ControllerBase
    {
        private readonly ILangRepository langRepository;

        public LangController(ILangRepository langRepository)
        {
            this.langRepository = langRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<LangVM>>> Search(string name)
        {
            try
            {
                var result = await langRepository.Search(name);

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
        public async Task<ActionResult> GetLangs()
        {
            try
            {
                return Ok(await langRepository.GetLangs());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LangVM>> GetLang(int id)
        {
            try
            {
                var result = await langRepository.GetLang(id);

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
        public async Task<ActionResult<LangVM>> CreateLang(LangVM langVM)
        {
            try
            {
                if (langVM == null)return BadRequest();

                // Add custom model validation error
                Lang lang = await langRepository.GetLangByname(langVM.Lang);
                if (lang != null)
                {
                    ModelState.AddModelError("Name", $"Lang name: {langVM.Lang.Name} already in use");
                    return BadRequest(ModelState);
                }

                await langRepository.CreateLang(langVM);

                return CreatedAtAction(nameof(GetLang),
                    new { id = langVM.Lang.Id }, langVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<LangVM>> UpdateLang(int id, LangVM langVM)
        {
            try
            {
                if (id != langVM.Lang.Id)
                    return BadRequest("Lang ID mismatch");

                // Add custom model validation error
                Lang lang = await langRepository.GetLangByname(langVM.Lang);
                if (lang != null)
                {
                    ModelState.AddModelError("Name", $"Lang name: {langVM.Lang.Name} already in use");
                    return BadRequest(ModelState);
                }

                var langToUpdate = await langRepository.GetLang(id);

                if (langToUpdate == null)
                    return NotFound($"Lang with Id = {id} not found");

                await langRepository.UpdateLang(langVM);

                return CreatedAtAction(nameof(GetLang), new { id = langVM.Lang.Id }, langVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<LangVM>> DeleteLang(int id)
        {
            try
            {
                var langToDelete = await langRepository.GetLang(id);

                if (langToDelete == null)
                {
                    return NotFound($"Lang with Id = {id} not found");
                }

                return await langRepository.DeleteLang(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
