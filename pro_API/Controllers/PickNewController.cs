using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class PickNewController : ControllerBase
    {
        private readonly IPickNewRepository picknewRepository;
        private readonly UserManager<IdentityUser> userManager;

        public PickNewController(IPickNewRepository picknewRepository, UserManager<IdentityUser> userManager)
        {
            this.picknewRepository = picknewRepository;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<List<VocCardVM>>> GetVocCardVMs(UserNameVM userNameVM)
        {
            try
            {
                var user = await userManager.FindByNameAsync(userNameVM.UserName);
                userNameVM.UserId = user.Id;

                var result = await picknewRepository.GetVocCardVMs(userNameVM);

                if (result == null) return NotFound();

                return result;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
      
    }
}
