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
    public class InfluencerController : ControllerBase
    {
        private readonly IInfluencerRepository influencerRepository;

        public InfluencerController(IInfluencerRepository influencerRepository)
        {
            this.influencerRepository = influencerRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<InfluencerVM>>> Search(string name)
        {
            try
            {
                var result = await influencerRepository.Search(name);

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
        public async Task<ActionResult> GetInfluencers()
        {
            try
            {
                return Ok(await influencerRepository.GetInfluencers());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<InfluencerVM>> GetInfluencer(int id)
        {
            try
            {
                var result = await influencerRepository.GetInfluencer(id);

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
        public async Task<ActionResult<InfluencerVM>> CreateInfluencer(InfluencerVM influencerVM)
        {
            try
            {
                if (influencerVM == null)return BadRequest();

                // Add custom model validation error
                Influencer influencer = await influencerRepository.GetInfluencerByname(influencerVM.Influencer);
                if (influencer != null)
                {
                    ModelState.AddModelError("Name", $"Influencer name: {influencerVM.Influencer.Name} already in use");
                    return BadRequest(ModelState);
                }

                await influencerRepository.CreateInfluencer(influencerVM);

                return CreatedAtAction(nameof(GetInfluencer),
                    new { id = influencerVM.Influencer.Id }, influencerVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<InfluencerVM>> UpdateInfluencer(int id, InfluencerVM influencerVM)
        {
            try
            {
                if (id != influencerVM.Influencer.Id)
                    return BadRequest("Influencer ID mismatch");

                // Add custom model validation error
                Influencer influencer = await influencerRepository.GetInfluencerByname(influencerVM.Influencer);
                if (influencer != null)
                {
                    ModelState.AddModelError("Name", $"Influencer name: {influencerVM.Influencer.Name} already in use");
                    return BadRequest(ModelState);
                }

                var influencerToUpdate = await influencerRepository.GetInfluencer(id);

                if (influencerToUpdate == null)
                    return NotFound($"Influencer with Id = {id} not found");

                await influencerRepository.UpdateInfluencer(influencerVM);

                return CreatedAtAction(nameof(GetInfluencer), new { id = influencerVM.Influencer.Id }, influencerVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<InfluencerVM>> DeleteInfluencer(int id)
        {
            try
            {
                var influencerToDelete = await influencerRepository.GetInfluencer(id);

                if (influencerToDelete == null)
                {
                    return NotFound($"Influencer with Id = {id} not found");
                }

                return await influencerRepository.DeleteInfluencer(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
