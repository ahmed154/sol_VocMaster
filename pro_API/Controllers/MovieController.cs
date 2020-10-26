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
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<MovieVM>>> Search(string name)
        {
            try
            {
                var result = await movieRepository.Search(name);

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
        public async Task<ActionResult> GetMovies()
        {
            try
            {
                return Ok(await movieRepository.GetMovies());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieVM>> GetMovie(int id)
        {
            try
            {
                var result = await movieRepository.GetMovie(id);

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
        public async Task<ActionResult<MovieVM>> CreateMovie(MovieVM movieVM)
        {
            try
            {
                if (movieVM == null)return BadRequest();

                // Add custom model validation error
                Movie movie = await movieRepository.GetMovieByname(movieVM.Movie);
                if (movie != null)
                {
                    ModelState.AddModelError("Name", $"Movie MovieId: {movieVM.Movie.MovieId} already in use");
                    return BadRequest(ModelState);
                }

                await movieRepository.CreateMovie(movieVM);

                return CreatedAtAction(nameof(GetMovie),
                    new { id = movieVM.Movie.Id }, movieVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<MovieVM>> UpdateMovie(int id, MovieVM movieVM)
        {
            try
            {
                if (id != movieVM.Movie.Id)
                    return BadRequest("Movie ID mismatch");

                // Add custom model validation error
                Movie movie = await movieRepository.GetMovieByname(movieVM.Movie);
                if (movie != null)
                {
                    ModelState.AddModelError("Name", $"Movie Movie Id: {movieVM.Movie.MovieId} already in use");
                    return BadRequest(ModelState);
                }

                var movieToUpdate = await movieRepository.GetMovie(id);

                if (movieToUpdate == null)
                    return NotFound($"Movie with Id = {id} not found");

                await movieRepository.UpdateMovie(movieVM);

                return CreatedAtAction(nameof(GetMovie), new { id = movieVM.Movie.Id }, movieVM);}

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<MovieVM>> DeleteMovie(int id)
        {
            try
            {
                var movieToDelete = await movieRepository.GetMovie(id);

                if (movieToDelete == null)
                {
                    return NotFound($"Movie with Id = {id} not found");
                }

                return await movieRepository.DeleteMovie(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
