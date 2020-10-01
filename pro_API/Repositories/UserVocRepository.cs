using Microsoft.EntityFrameworkCore;
using pro_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pro_API.Repositories;
using pro_Models.Models;
using pro_Models.ViewModels;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace pro_API.Repositories
{
    public class UserVocRepository : IUserVocRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public UserVocRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        public async Task<List<UserVocVM>> GetVocStudy(string userId)
        {
            List<UserVocVM> userVocVMs = new List<UserVocVM>();

            List<UserVoc> userVocs = await appDbContext.UserVocs
                .Where(x => x.UserId == userId && x.Study == false)
                .Include(x => x.Voc)
                .OrderBy(x => x.NextReviewTime)
                .ToListAsync();

            if (userVocs != null)
            {
                foreach (var userVoc in userVocs)
                {
                    userVocVMs.Add(new UserVocVM { UserVoc = userVoc });
                }
            }

            return userVocVMs;
        }
        public async Task<List<UserVocVM>> GetVocCheck(string userId)
        {
            List<UserVocVM> userVocVMs = new List<UserVocVM>();

            List<UserVoc> userVocs = await appDbContext.UserVocs
                .Where(x => x.UserId == userId && x.NextReviewTime < DateTime.UtcNow && x.Study == true)
                .Include(x => x.Voc)
                .OrderBy(x => x.NextReviewTime).ToListAsync();

            if (userVocs != null)
            {
                foreach (var userVoc in userVocs)
                {
                    userVocVMs.Add(new UserVocVM { UserVoc = userVoc });
                }
            }

            return userVocVMs;
        }

        async Task<List<UserVocVM>> IUserVocRepository.Search(string name)
        {
            List<UserVocVM> uservocVMs = new List<UserVocVM>();

            IQueryable<UserVoc> query = appDbContext.UserVocs;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Voc.Text.Contains(name));
            }

            var uservocs = await query.ToListAsync();

            foreach (var uservoc in uservocs)
            {
                uservocVMs.Add(new UserVocVM { UserVoc = uservoc });
            }
            return uservocVMs;
        }
        public async Task<List<UserVocVM>> GetUserVocs()
        {
            List<UserVocVM> uservocVMs = new List<UserVocVM>();
            var uservocs = await appDbContext.UserVocs.ToListAsync();
            foreach (var uservoc in uservocs)
            {
                uservocVMs.Add(new UserVocVM { UserVoc = uservoc});
            }
            return uservocVMs; 
        }
        public async Task<UserVocVM> GetUserVoc(int id)
        {
            UserVocVM uservocVM = new UserVocVM();
            //uservocVM.UserVoc = await appDbContext.UserVocs.FirstOrDefaultAsync(e => e.Id == id);
            return uservocVM;
        }
        public async Task<UserVocListVM> GetStudyVM(UserVocListVM studyVM)
        {
            studyVM.UserVocs = await appDbContext.UserVocs
                .Include(x => x.Voc)
                .Where(x => x.UserId == studyVM.UserId && x.Study == false)
                .ToListAsync();
            return studyVM;
        }
        public async Task<UserVocListVM> GetReviewVM(UserVocListVM reviewVM)
        {
            reviewVM.UserVocs = await appDbContext.UserVocs
                .Include(x => x.Voc)
                .Where(x => x.UserId == reviewVM.UserId && x.Study == true && x.NextReviewTime < DateTime.UtcNow)
                .ToListAsync();
            return reviewVM;
        }
        public async Task<UserVocVM> CreateUserVoc(UserVocVM uservocVM)
        {
            var result = await appDbContext.UserVocs.AddAsync(uservocVM.UserVoc);
            await appDbContext.SaveChangesAsync();

            uservocVM.UserVoc = result.Entity;
            return uservocVM;
        }
        public async Task CreateUserVocs(UserVocListVM userVocVMsCreate)
        {
            UserInfo userInfo = await appDbContext.UserInfos.FirstOrDefaultAsync(x => x.UserId == userVocVMsCreate.UserId);
            if (userInfo != null)
            {
                appDbContext.Entry(userInfo).State = EntityState.Detached;
                userInfo.LastVocId = userVocVMsCreate.LastVocId;
                appDbContext.Entry(userInfo).State = EntityState.Modified;
            }
            else
            { return; }


            foreach (var uservoc in userVocVMsCreate.UserVocs)
            {
                appDbContext.Entry(uservoc.Voc).State = EntityState.Detached;
                await appDbContext.UserVocs.AddAsync(uservoc);
                appDbContext.Entry(uservoc.Voc).State = EntityState.Modified;
            }
 
            await appDbContext.SaveChangesAsync();
        }
        public async Task<UserVocVM> UpdateUserVoc(UserVocVM uservocVM)
        {
            //UserVoc result = await appDbContext.UserVocs
            //    .FirstOrDefaultAsync(e => e.Id == uservocVM.UserVoc.Id);

            //if (result != null)
            //{
            //    appDbContext.Entry(result).State = EntityState.Detached;
            //    result = mapper.Map(uservocVM.UserVoc, result);
            //    appDbContext.Entry(result).State = EntityState.Modified;

            //    await appDbContext.SaveChangesAsync();
            //    return new UserVocVM { UserVoc = result };
            //}

            return null;
        }
        public async Task UpdateUserVocList(UserVocListVM userVocVMsUpdate)
        {
            List<UserVoc> dbUservocs = new List<UserVoc>();
            foreach (var userVoc in userVocVMsUpdate.UserVocs)
            {
                dbUservocs.Add(await appDbContext.UserVocs.FirstOrDefaultAsync(x => x.UserId == userVoc.UserId && x.VocId == userVoc.VocId) );
            }

            foreach (var dbUservoc in dbUservocs)
            {
                if(dbUservoc != null) dbUservoc.Study = true;
            }

            await appDbContext.SaveChangesAsync();                       
        }
        public async Task UpdateUserVocsReview(UserVocListVM userVocVMsUpdate)
        {
            List<UserVoc> dbUservocs = new List<UserVoc>();
            UserVoc dbUserVoc = new UserVoc();
            foreach (var userVoc in userVocVMsUpdate.UserVocs)
            {
                dbUserVoc = await appDbContext.UserVocs.FirstOrDefaultAsync(x => x.UserId == userVoc.UserId && x.VocId == userVoc.VocId);
                dbUserVoc.Repetition = userVoc.Repetition;
                dbUserVoc.Success = userVoc.Success;
                dbUserVoc.NextReviewTime = userVoc.NextReviewTime;
                dbUserVoc.Study = userVoc.Study;
                dbUserVoc.LevelCounter = userVoc.LevelCounter;
                dbUserVoc.Level = userVoc.Level;

                dbUservocs.Add(dbUserVoc);
            }

            await appDbContext.SaveChangesAsync();
        }

        public async Task<VocCardVM> AddVocCardVM(VocCardVM vocCardVM)
        {
            using (var dbContextTransaction = appDbContext.Database.BeginTransaction())
            {
                try
                {
                    await appDbContext.VocAudios.AddRangeAsync(vocCardVM.VocAudios);
                    await appDbContext.Definitions.AddRangeAsync(vocCardVM.Definitions);
                    await appDbContext.Synonyms.AddRangeAsync(vocCardVM.Synonyms);
                    await appDbContext.Translates.AddRangeAsync(vocCardVM.Translates);

                    await appDbContext.SaveChangesAsync();

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                 {

                    throw;
                }
            }

//await appDbContext.SaveChangesAsync();
;
            return vocCardVM;
        }
        //public async Task<List<VocCardVM>> GetVocCardVMs()
        //{
        //    List<VocCardVM> vocCardVMs = new List<VocCardVM>();
        //    VocCardVM vocCardVM = new VocCardVM();
        //    vocCardVM.UserId = "e4b7bb16-7ebd-4b3c-9025-68af7e9f02f2";
        //    List<UserVoc> userVocs = new List<UserVoc>();

        //    userVocs = await appDbContext.UserVocs.Where(x => x.UserId == vocCardVM.UserId && x.Study == true).ToListAsync();

        //    foreach (var userVoc in userVocs)
        //    {
        //        vocCardVM.Voc = await appDbContext.Vocs.FirstOrDefaultAsync(x => x.Id == userVoc.VocId);
        //        vocCardVM.VocAudios = await appDbContext.VocAudios.Where(x => x.VocId == userVoc.VocId).ToListAsync();
        //        vocCardVM.Definitions = await appDbContext.Definitions.Where(x => x.VocId == userVoc.VocId).Include(e => e.Examples).ToListAsync();
        //        vocCardVM.Synonyms = await appDbContext.Synonyms.Where(x => x.VocId == userVoc.VocId).ToListAsync();
        //        vocCardVM.Translates = await appDbContext.Translates.Where(x => x.VocId == userVoc.VocId).ToListAsync();

        //        vocCardVMs.Add(vocCardVM);
        //    }

        //    return vocCardVMs;
        //}
        public async Task<List<VocCardVM>> GetVocCardVMs(UserNameVM userNameVM)
        {
            List<VocCardVM> vocCardVMs = new List<VocCardVM>();
            List<UserVoc> userVocs = await appDbContext.UserVocs.Where(x => x.UserId == userNameVM.UserId && x.Study == false).ToListAsync();

            foreach (var userVoc in userVocs)
            {
                vocCardVMs.Add(new VocCardVM 
                {
                    Voc = await appDbContext.Vocs.FirstOrDefaultAsync(x => x.Id == userVoc.VocId),
                    UserVoc = userVoc,
                    VocAudios = await appDbContext.VocAudios.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Definitions = await appDbContext.Definitions.Where(x => x.VocId == userVoc.VocId).Include(e => e.Examples).ToListAsync(),
                    Synonyms = await appDbContext.Synonyms.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Translates = await appDbContext.Translates.Where(x => x.VocId == userVoc.VocId).ToListAsync()
                });
            }

            return vocCardVMs;
        }



        public async Task<UserVocListVM> GetUserVocVMsResults(UserNameVM userNameVM)
        {
            UserVocListVM userVocListVMs = new UserVocListVM();

            userVocListVMs.UserVocs = await appDbContext.UserVocs
                .Include(x => x.Voc)
                .Where(x => x.UserId == userNameVM.UserId)
                .ToListAsync();
            return userVocListVMs;
        }


        public async Task<UserVocVM> DeleteUserVoc(int uservocId)
        {
            //var result = await appDbContext.UserVocs
            //    .FirstOrDefaultAsync(e => e.Id == uservocId);
            //if (result != null)
            //{
            //    appDbContext.UserVocs.Remove(result);
            //    await appDbContext.SaveChangesAsync();
            //    return new UserVocVM { UserVoc = result };
            //}

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<UserVoc> GetUserVocByname(UserVoc uservoc)
        {
             uservoc = await appDbContext.UserVocs.Where(n => n.UserId == uservoc.UserId && n.VocId != uservoc.VocId)
                .FirstOrDefaultAsync();

            return uservoc;
        }
    }
}
