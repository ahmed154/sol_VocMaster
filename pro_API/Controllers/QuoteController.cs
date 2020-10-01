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
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteRepository quoteRepository;

        public QuoteController(IQuoteRepository quoteRepository)
        {
            this.quoteRepository = quoteRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<QuoteVM>>> Search(string name)
        {
            try
            {
                var result = await quoteRepository.Search(name);

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
        public async Task<ActionResult> GetQuotes()
        {
            try
            {
                return Ok(await quoteRepository.GetQuotes());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuoteVM>> GetQuote(int id)
        {
            try
            {
                var result = await quoteRepository.GetQuote(id);

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
        public async Task<ActionResult<QuoteVM>> CreateQuote(QuoteVM quoteVM)
        {
            try
            {
                if (quoteVM == null)return BadRequest();

                // Add custom model validation error
                Quote quote = await quoteRepository.GetQuoteByname(quoteVM.Quote);
                if (quote != null)
                {
                    ModelState.AddModelError("Name", $"Quote name: {quoteVM.Quote.Text} already in use");
                    return BadRequest(ModelState);
                }

                await quoteRepository.CreateQuote(quoteVM);

                return CreatedAtAction(nameof(GetQuote),
                    new { id = quoteVM.Quote.Id }, quoteVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<QuoteVM>> UpdateQuote(int id, QuoteVM quoteVM)
        {
            try
            {
                if (id != quoteVM.Quote.Id)
                    return BadRequest("Quote ID mismatch");

                // Add custom model validation error
                Quote quote = await quoteRepository.GetQuoteByname(quoteVM.Quote);
                if (quote != null)
                {
                    ModelState.AddModelError("Name", $"Quote name: {quoteVM.Quote.Text} already in use");
                    return BadRequest(ModelState);
                }

                var quoteToUpdate = await quoteRepository.GetQuote(id);

                if (quoteToUpdate == null)
                    return NotFound($"Quote with Id = {id} not found");

                await quoteRepository.UpdateQuote(quoteVM);

                return CreatedAtAction(nameof(GetQuote), new { id = quoteVM.Quote.Id }, quoteVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<QuoteVM>> DeleteQuote(int id)
        {
            try
            {
                var quoteToDelete = await quoteRepository.GetQuote(id);

                if (quoteToDelete == null)
                {
                    return NotFound($"Quote with Id = {id} not found");
                }

                return await quoteRepository.DeleteQuote(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
