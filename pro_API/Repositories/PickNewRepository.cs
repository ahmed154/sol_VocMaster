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
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace pro_API.Repositories
{
    public class PickNewRepository : IPickNewRepository
    {
        private readonly AppDbContext appDbContext;

        public PickNewRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<PickNewVM> GetPickNew(PickNewVM pickNewVM)
        {
            UserInfo userInfo = await appDbContext.UserInfos.FirstOrDefaultAsync(x => x.UserId == pickNewVM.UserId);

            if(userInfo == null)
            {
                pickNewVM.LastVocId = 0;
            }
            else
            {
                pickNewVM.LastVocId = userInfo.LastVocId;
            }

            var vocs = await appDbContext.Vocs.Where(x=>x.Id > pickNewVM.LastVocId).Take(pickNewVM.Take).ToListAsync();

            pickNewVM.Vocs = vocs;
            
            return pickNewVM; 
        }
        public async Task<List<VocCardVM>> GetVocCardVMs(UserNameVM userNameVM)
        {
            UserInfo userInfo = await appDbContext.UserInfos.FirstOrDefaultAsync(x => x.UserId == userNameVM.UserId);
            List<VocCardVM> vocCardVMs = new List<VocCardVM>();

            if (userInfo != null) userNameVM.LastVocId = userInfo.LastVocId;

            var vocs = await appDbContext.Vocs.Where(x => x.Id > userInfo.LastVocId).Take(50).ToListAsync();

            foreach (var voc in vocs)
            {
                vocCardVMs.Add(new VocCardVM
                { 
                    Voc = voc,
                    VocAudios = await appDbContext.VocAudios.Where(x => x.VocId == voc.Id).ToListAsync(),
                    Definitions = await appDbContext.Definitions.Where(x => x.VocId == voc.Id).Include(e => e.Examples).ToListAsync(),
                    Synonyms = await appDbContext.Synonyms.Where(x => x.VocId == voc.Id).ToListAsync(),
                    Translates = await appDbContext.Translates.Where(x => x.VocId == voc.Id).ToListAsync()
                });
            }

            return vocCardVMs;
        }

    }
}
