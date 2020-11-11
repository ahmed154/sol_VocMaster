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
    public class VocMasterController : ControllerBase
    {
        private readonly IVocMasterRepository vocMasterRepository;
        private readonly UserManager<IdentityUser> userManager;

        public VocMasterController(IVocMasterRepository VocMasterRepository, UserManager<IdentityUser> userManager)
        {
            vocMasterRepository = VocMasterRepository;
            this.userManager = userManager;
        }

        [HttpPost("GetVocMasterVM")]
        public async Task<ActionResult<VocMasterVM>> GetVocMasterVM(UserNameVM userNameVM)
        {
            try
            {
                if (userNameVM == null) return BadRequest();

                var user = await userManager.FindByNameAsync(userNameVM.UserName);
                userNameVM.UserId = user.Id;

                return await vocMasterRepository.GetVocMasterVM(userNameVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("UpdateVocMasterVM")]
        public async Task<ActionResult<VocMasterVM>> UpdateVocMasterVM(VocMasterVM vocMasterVM)
        {
            try
            {
                if (vocMasterVM == null) return BadRequest();

                var user = await userManager.FindByNameAsync(vocMasterVM.UserInfo.UserName);
                vocMasterVM.UserInfo.UserId = user.Id;

                return await vocMasterRepository.UpdateVocMasterVM(vocMasterVM);
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
                if (vocVM == null) return BadRequest();

                vocVM = await vocMasterRepository.GetVocVMByText(vocVM);
                return vocVM;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
