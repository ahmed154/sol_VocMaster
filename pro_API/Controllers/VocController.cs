using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pro_API.Repositories;
using pro_Models.Models;
using pro_Models.ViewModels;

namespace pro_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocController : ControllerBase
    {
        private readonly IVocRepository vocRepository;
        private readonly UserManager<IdentityUser> userManager;

        public VocController(IVocRepository vocRepository, UserManager<IdentityUser> userManager)
        {
            this.vocRepository = vocRepository;
            this.userManager = userManager;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<VocVM>>> Search(string name)
        {
            try
            {
                var result = await vocRepository.Search(name);

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
        public async Task<ActionResult<List<VocVM>>> GetVocs()
        {
            try
            {
                return Ok(await vocRepository.GetVocs());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VocVM>> GetVoc(int id)
        {
            try
            {
                var result = await vocRepository.GetVoc(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPost("GetVocVMByText")]
        public async Task<ActionResult<VocVM>> GetVocVMByText(VocVM vocVM)
        {
            try
            {
                if (vocVM == null || string.IsNullOrEmpty(vocVM.Voc.Text)) return NotFound();

                var result = await vocRepository.GetVocVMByText(vocVM);

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
        public async Task<ActionResult<VocVM>> CreateVoc(VocVM vocVM)
        {
            try
            {
                if (vocVM == null)return BadRequest();

                // Add custom model validation error
                Voc voc = await vocRepository.GetVocByname(vocVM.Voc);
                if (voc != null)
                {
                    ModelState.AddModelError("Name", $"Voc name: {vocVM.Voc.Text} already in use");
                    return BadRequest(ModelState);
                }

                await vocRepository.CreateVoc(vocVM);

                return CreatedAtAction(nameof(GetVoc),
                    new { id = vocVM.Voc.Id }, vocVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<VocVM>> UpdateVoc(int id, VocVM vocVM)
        {
            try
            {
                if (id != vocVM.Voc.Id)
                    return BadRequest("Voc ID mismatch");

                // Add custom model validation error
                Voc voc = await vocRepository.GetVocByname(vocVM.Voc);
                if (voc != null)
                {
                    ModelState.AddModelError("Name", $"Voc name: {vocVM.Voc.Text} already in use");
                    return BadRequest(ModelState);
                }

                var vocToUpdate = await vocRepository.GetVoc(id);

                if (vocToUpdate == null)
                    return NotFound($"Voc with Id = {id} not found");

                await vocRepository.UpdateVoc(vocVM);

                return CreatedAtAction(nameof(GetVoc), new { id = vocVM.Voc.Id }, vocVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<VocVM>> DeleteVoc(int id)
        {
            try
            {
                var vocToDelete = await vocRepository.GetVoc(id);

                if (vocToDelete == null)
                {
                    return NotFound($"Voc with Id = {id} not found");
                }

                return await vocRepository.DeleteVoc(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }


        [HttpPost("GetControlVocCardVMs")]
        public async Task<ActionResult<List<VocCardVM>>> GetControlVocCardVMs(VocCardVM vocCardVM)
        {
            try
            {
                return await vocRepository.GetControlVocCardVMs(vocCardVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
