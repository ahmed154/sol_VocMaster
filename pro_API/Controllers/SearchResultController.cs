using System;
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
    public class SearchResultController : ControllerBase
    {
        private readonly ISearchResultRepository searchResultRepository;
        private readonly UserManager<IdentityUser> userManager;

        public SearchResultController(ISearchResultRepository SearchResultRepository, UserManager<IdentityUser> userManager)
        {
            searchResultRepository = SearchResultRepository;
            this.userManager = userManager;
        }

        [HttpPost("GetImages")]
        public async Task<ActionResult<List<Image>>> GetImages(VocVM vocVM)
        {
            try
            {
                if (vocVM == null) return BadRequest();

                return await searchResultRepository.GetImages(vocVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }

        [HttpPost("GetVids")]
        public async Task<ActionResult<List<Vid>>> GetVids(VocVM vocVM)
        {
            try
            {
                if (vocVM == null) return BadRequest();

                return await searchResultRepository.GetVids(vocVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
