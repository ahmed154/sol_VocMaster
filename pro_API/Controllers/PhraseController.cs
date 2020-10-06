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
    public class PhraseController : ControllerBase
    {
        private readonly IPhraseRepository phraseRepository;

        public PhraseController(IPhraseRepository phraseRepository)
        {
            this.phraseRepository = phraseRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<PhraseVM>>> Search(string name)
        {
            try
            {
                var result = await phraseRepository.Search(name);

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
        public async Task<ActionResult> GetPhrases()
        {
            try
            {
                return Ok(await phraseRepository.GetPhrases());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PhraseVM>> GetPhrase(int id)
        {
            try
            {
                var result = await phraseRepository.GetPhrase(id);

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
        public async Task<ActionResult<PhraseVM>> CreatePhrase(PhraseVM phraseVM)
        {
            try
            {
                if (phraseVM == null)return BadRequest();

                // Add custom model validation error
                Phrase phrase = await phraseRepository.GetPhraseByname(phraseVM.Phrase);
                if (phrase != null)
                {
                    ModelState.AddModelError("Name", $"Phrase name: {phraseVM.Phrase.Text} already in use");
                    return BadRequest(ModelState);
                }

                await phraseRepository.CreatePhrase(phraseVM);

                return CreatedAtAction(nameof(GetPhrase),
                    new { id = phraseVM.Phrase.Id }, phraseVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PhraseVM>> UpdatePhrase(int id, PhraseVM phraseVM)
        {
            try
            {
                if (id != phraseVM.Phrase.Id)
                    return BadRequest("Phrase ID mismatch");

                // Add custom model validation error
                Phrase phrase = await phraseRepository.GetPhraseByname(phraseVM.Phrase);
                if (phrase != null)
                {
                    ModelState.AddModelError("Name", $"Phrase name: {phraseVM.Phrase.Text} already in use");
                    return BadRequest(ModelState);
                }

                var phraseToUpdate = await phraseRepository.GetPhrase(id);

                if (phraseToUpdate == null)
                    return NotFound($"Phrase with Id = {id} not found");

                await phraseRepository.UpdatePhrase(phraseVM);

                return CreatedAtAction(nameof(GetPhrase), new { id = phraseVM.Phrase.Id }, phraseVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PhraseVM>> DeletePhrase(int id)
        {
            try
            {
                var phraseToDelete = await phraseRepository.GetPhrase(id);

                if (phraseToDelete == null)
                {
                    return NotFound($"Phrase with Id = {id} not found");
                }

                return await phraseRepository.DeletePhrase(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
