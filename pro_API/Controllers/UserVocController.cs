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
    public class UserVocController : ControllerBase
    {
        private readonly IUserVocRepository uservocRepository;
        private readonly UserManager<IdentityUser> userManager;

        public UserVocController(IUserVocRepository uservocRepository, UserManager<IdentityUser> userManager)
        {
            this.uservocRepository = uservocRepository;
            this.userManager = userManager;
        }

        [HttpPost("study")]
        public async Task<ActionResult<List<UserVocVM>>> GetVocStudy(UserVocVM userVocVM)
        {
            try
            {
                var user = await userManager.FindByNameAsync(userVocVM.UserName);

                var result = await uservocRepository.GetVocStudy(user.Id);

                if (result == null) return NotFound();

                return result;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPost("check")]
        public async Task<ActionResult<List<UserVocVM>>> GetVocCheck(UserVocVM userVocVM)
        {
            try
            {
                var user = await userManager.FindByNameAsync(userVocVM.UserName);

                var result = await uservocRepository.GetVocCheck(user.Id);

                if (result == null) return NotFound();

                return result;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<UserVocVM>>> Search(string name)
        {
            try
            {
                var result = await uservocRepository.Search(name);

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
        public async Task<ActionResult> GetUserVocs()
        {
            try
            {
                return Ok(await uservocRepository.GetUserVocs());
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserVocVM>> GetUserVoc(int id)
        {
            try
            {
                var result = await uservocRepository.GetUserVoc(id);

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
        public async Task<ActionResult<UserVocVM>> CreateUserVoc(UserVocVM uservocVM)
        {
            try
            {
                if (uservocVM == null)return BadRequest();

                var user = await userManager.FindByNameAsync(uservocVM.UserVoc.UserId);
                uservocVM.UserVoc.UserId = user.Id;

                await uservocRepository.CreateUserVoc(uservocVM);

                return null;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPost("list")]
        public async Task<ActionResult> CreateUserVocs(UserVocListVM userVocVMsCreate)
        {
            try
            {
                if (userVocVMsCreate == null) return BadRequest();

                var user = await userManager.FindByNameAsync(userVocVMsCreate.UserName);
                userVocVMsCreate.UserId = user.Id;

                foreach (var uservoc in userVocVMsCreate.UserVocs)
                {
                    uservoc.UserId = userVocVMsCreate.UserId;
                }

                await uservocRepository.CreateUserVocs(userVocVMsCreate);

                return null;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserVocVM>> UpdateUserVoc(int id, UserVocVM uservocVM)
        {
            try
            {
                var uservocToUpdate = await uservocRepository.GetUserVoc(id);

                if (uservocToUpdate == null)
                    return NotFound($"UserVoc with Id = {id} not found");

                await uservocRepository.UpdateUserVoc(uservocVM);

                return null;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("list")]
        public async Task<ActionResult> UpdateUserVocList(UserVocListVM userVocVMsUpdate)
        {
            try
            {
                if (userVocVMsUpdate == null)
                    return BadRequest("userVocVMsUpdate mismatch");

                var user = await userManager.FindByNameAsync(userVocVMsUpdate.UserName);
                userVocVMsUpdate.UserId = user.Id;

                foreach (var uservoc in userVocVMsUpdate.UserVocs)
                {
                    uservoc.UserId = userVocVMsUpdate.UserId;
                }

                await uservocRepository.UpdateUserVocList(userVocVMsUpdate);

                return null;
            }

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPut("review")]
        public async Task<ActionResult> UpdateUserVocsReview(UserVocListVM userVocVMsUpdate)
        {
            try
            {
                if (userVocVMsUpdate == null)
                    return BadRequest("userVocVMsUpdate mismatch");

                var user = await userManager.FindByNameAsync(userVocVMsUpdate.UserName);
                userVocVMsUpdate.UserId = user.Id;

                foreach (var uservoc in userVocVMsUpdate.UserVocs)
                {
                    uservoc.UserId = userVocVMsUpdate.UserId;
                }

                await uservocRepository.UpdateUserVocsReview(userVocVMsUpdate);

                return null;
            }

            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPost("GetReviewVM")]
        public async Task<ActionResult<UserVocListVM>> GetReviewVM(UserVocListVM reviewVM)
        {
            try
            {
                if (reviewVM == null) return BadRequest();

                var user = await userManager.FindByNameAsync(reviewVM.UserName);
                reviewVM.UserId = user.Id;

                await uservocRepository.GetReviewVM(reviewVM);

                return reviewVM;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPost("GetStudyVM")]
        public async Task<ActionResult<UserVocListVM>> GetStudyVM(UserVocListVM studyVM)
        {
            try
            {
                if (studyVM == null) return BadRequest();

                var user = await userManager.FindByNameAsync(studyVM.UserName);
                studyVM.UserId = user.Id;

                await uservocRepository.GetStudyVM(studyVM);

                return studyVM;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPost("AddVocCardVM")]
        public async Task<ActionResult<VocCardVM>> AddVocCardVM(VocCardVM vocCardVM)
        {
            try
            {
                if (vocCardVM == null) return BadRequest();

                await uservocRepository.AddVocCardVM(vocCardVM);

                return vocCardVM;
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }

        [HttpPost("GetVocCardVMs")]
        public async Task<ActionResult<List<VocCardVM>>> GetVocCardVMs(UserNameVM userNameVM)
        {
            try
            {
                if (userNameVM == null) return BadRequest();

                var user = await userManager.FindByNameAsync(userNameVM.UserName);
                userNameVM.UserId = user.Id;

                return await uservocRepository.GetVocCardVMs(userNameVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpPost("GetUserVocVMsResults")]
        public async Task<ActionResult<UserVocListVM>> GetUserVocVMsResults(UserNameVM userNameVM)
        {
            try
            {
                if (userNameVM == null) return BadRequest();

                var user = await userManager.FindByNameAsync(userNameVM.UserName);
                userNameVM.UserId = user.Id;

                return await uservocRepository.GetUserVocVMsResults(userNameVM);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserVocVM>> DeleteUserVoc(int id)
        {
            try
            {
                var uservocToDelete = await uservocRepository.GetUserVoc(id);

                if (uservocToDelete == null)
                {
                    return NotFound($"UserVoc with Id = {id} not found");
                }

                return await uservocRepository.DeleteUserVoc(id);
            }
            catch (DbUpdateException Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Ex.InnerException.Message);
            }
        }
    }
}
