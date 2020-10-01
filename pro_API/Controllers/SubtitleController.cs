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
    public class SubtitleController : ControllerBase
    {
        private readonly ISubtitleRepository subtitleRepository;

        public SubtitleController(ISubtitleRepository subtitleRepository)
        {
            this.subtitleRepository = subtitleRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<SubtitleVM>>> Search(string name)
        {
            try
            {
                var result = await subtitleRepository.Search(name);

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
        public async Task<ActionResult> GetSubtitles()
        {
            try
            {
                return Ok(await subtitleRepository.GetSubtitles());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubtitleVM>> GetSubtitle(int id)
        {
            try
            {
                var result = await subtitleRepository.GetSubtitle(id);

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
        public async Task<ActionResult<SubtitleVM>> CreateSubtitle(SubtitleVM subtitleVM)
        {
            try
            {
                if (subtitleVM == null)return BadRequest();

                await subtitleRepository.CreateSubtitle(subtitleVM);

                return CreatedAtAction(nameof(GetSubtitle),
                    new { id = subtitleVM.Subtitle.Id }, subtitleVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SubtitleVM>> UpdateSubtitle(int id, SubtitleVM subtitleVM)
        {
            try
            {
                if (id != subtitleVM.Subtitle.Id)
                    return BadRequest("Subtitle ID mismatch");

                var subtitleToUpdate = await subtitleRepository.GetSubtitle(id);

                if (subtitleToUpdate == null)
                    return NotFound($"Subtitle with Id = {id} not found");

                await subtitleRepository.UpdateSubtitle(subtitleVM);

                return CreatedAtAction(nameof(GetSubtitle), new { id = subtitleVM.Subtitle.Id }, subtitleVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<SubtitleVM>> DeleteSubtitle(int id)
        {
            try
            {
                var subtitleToDelete = await subtitleRepository.GetSubtitle(id);

                if (subtitleToDelete == null)
                {
                    return NotFound($"Subtitle with Id = {id} not found");
                }

                return await subtitleRepository.DeleteSubtitle(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }


        [HttpPost("CalcSubtitlesVM")]
        public async Task<ActionResult<List<SubtitleVM>>> CalcSubtitlesVM(Voc voc)
        {
            try
            {
                if (voc == null) return BadRequest();

                return await subtitleRepository.CalcSubtitlesVM(voc);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
