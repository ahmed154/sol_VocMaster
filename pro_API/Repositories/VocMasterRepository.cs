using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pro_API.Data;
using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public class VocMasterRepository : IVocMasterRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public VocMasterRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        #region Pro
        public async Task<List<VocCardVM>> GetNews(UserInfo userInfo)
        {
            List<VocCardVM> vocCardVMs = new List<VocCardVM>();
            var vocs = await appDbContext.Vocs.Where(x => x.Id > userInfo.LastVocId).Take(3).ToListAsync();

            foreach (var voc in vocs)
            {
                vocCardVMs.Add(new VocCardVM
                {
                    Voc = voc,
                    VocAudios = await appDbContext.VocAudios.Where(x => x.VocId == voc.Id).ToListAsync(),
                    Definitions = await appDbContext.Definitions.Where(x => x.VocId == voc.Id).Include(e => e.Examples).ToListAsync(),
                    Images = await appDbContext.Images.Where(x => x.VocId == voc.Id).ToListAsync(),
                    Synonyms = await appDbContext.Synonyms.Where(x => x.VocId == voc.Id).ToListAsync(),
                    Translates = await appDbContext.Translates.Where(x => x.VocId == voc.Id).ToListAsync(),
                    Subtitles = await GetSubtitles(voc)
                });
            }

            return vocCardVMs;
        }
        public async Task<List<VocCardVM>> GetStudys(UserNameVM userNameVM)
        {
            List<VocCardVM> vocCardVMs = new List<VocCardVM>();
            List<UserVoc> userVocs = await appDbContext.UserVocs.Where(x => x.UserId == userNameVM.UserId && x.Study == false).ToListAsync();
            Movie movie = new Movie();

            foreach (var userVoc in userVocs)
            {
                vocCardVMs.Add(new VocCardVM
                {
                    Voc = await appDbContext.Vocs.FirstOrDefaultAsync(x => x.Id == userVoc.VocId),
                    UserVoc = userVoc,
                    VocAudios = await appDbContext.VocAudios.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Definitions = await appDbContext.Definitions.Where(x => x.VocId == userVoc.VocId).Include(e => e.Examples).ToListAsync(),
                    Images = await appDbContext.Images.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Synonyms = await appDbContext.Synonyms.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Translates = await appDbContext.Translates.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Subtitles = await GetSubtitles(userVoc.Voc)
                });
            }

            return vocCardVMs;
        }
        public async Task<List<VocCardVM>> GetTests(UserNameVM userNameVM)
        {
            List<VocCardVM> vocCardVMs = new List<VocCardVM>();
            List<UserVoc> userVocs = await appDbContext.UserVocs
                .Include(x => x.Voc)
                .Where(x => x.UserId == userNameVM.UserId && x.Study == true && x.NextReviewTime < DateTime.UtcNow)
                .ToListAsync();

            foreach (var userVoc in userVocs)
            {
                vocCardVMs.Add(new VocCardVM
                {
                    Voc = await appDbContext.Vocs.FirstOrDefaultAsync(x => x.Id == userVoc.VocId),
                    UserVoc = userVoc,
                    VocAudios = await appDbContext.VocAudios.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Definitions = await appDbContext.Definitions.Where(x => x.VocId == userVoc.VocId).Include(e => e.Examples).ToListAsync(),
                    Images = await appDbContext.Images.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Synonyms = await appDbContext.Synonyms.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Translates = await appDbContext.Translates.Where(x => x.VocId == userVoc.VocId).ToListAsync(),
                    Subtitles = await GetSubtitles(userVoc.Voc)
                });
            }

            return vocCardVMs;
        }
        public async Task<List<UserVoc>> GetResults(UserNameVM userNameVM)
        {
            return await appDbContext.UserVocs
                .Include(x => x.Voc)
                .Where(x => x.UserId == userNameVM.UserId)
                .ToListAsync();
        }
        async Task<List<Subtitle>> GetSubtitles(Voc voc)
        {
            List<VocSubtitle> vocSubtitles = await appDbContext.VocsSubtitless.Where(x => x.VocId == voc.Id).ToListAsync();
            List<Subtitle> subtitles = new List<Subtitle>();

            foreach (var vocSubtitle in vocSubtitles)
            {
                var subtitle = await appDbContext.Subtitles.Include(x => x.Movie).FirstOrDefaultAsync(x => x.Id == vocSubtitle.SubtitleId);

                subtitle.Text = subtitle.Text.Replace(voc.Text, $"<mark>{voc.Text}</mark>");
                subtitle.Movie.Text = null;
                subtitle.Movie.Subtitles = null;
                subtitle.VocSubtitles = null;
                subtitles.Add(subtitle);
            }

            return subtitles;
        }
        #endregion

        public async Task<VocMasterVM> GetVocMasterVM(UserNameVM userNameVM)
        {
            VocMasterVM vocMasterVM = new VocMasterVM();

            vocMasterVM.UserInfo = await appDbContext.UserInfos.FirstOrDefaultAsync(x => x.UserId == userNameVM.UserId);

            vocMasterVM.News = await GetNews(vocMasterVM.UserInfo);
            vocMasterVM.Studys = await GetStudys(userNameVM);
            vocMasterVM.Tests = await GetTests(userNameVM);
            vocMasterVM.Results = await GetResults(userNameVM);

            return vocMasterVM;
        }
        public async Task<VocMasterVM> UpdateVocMasterVM(VocMasterVM vocMasterVM)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// New
            if (vocMasterVM.News.Count > 0)
            {
                string userInfoUpdate = $@"UPDATE [dbo].[UserInfos]
                                                SET [LastVocId] = {vocMasterVM.UserInfo.LastVocId}                                                 
                                                WHERE [UserId] = '{vocMasterVM.UserInfo.UserId}';";

                string insertUserVoc = @"INSERT INTO [dbo].[UserVocs]
                                                ([UserId]
                                                ,[VocId]
                                                ,[Repetition]
                                                ,[Success]
                                                ,[NextReviewTime]
                                                ,[Study]
                                                ,[Level]
                                                ,[LevelCounter])
                                            VALUES";
                string values = "";
                foreach (var nnew in vocMasterVM.News)
                {
                    nnew.UserVoc.UserId = vocMasterVM.UserInfo.UserId;

                    values += @$",('{nnew.UserVoc.UserId}'
                                , {nnew.UserVoc.VocId}
                                , {nnew.UserVoc.Repetition}
                                , {nnew.UserVoc.Success}
                                , '{nnew.UserVoc.NextReviewTime.ToString("yyyy-MM-dd HH:mm:ss")}'
                                , '{nnew.UserVoc.Study}'
                                , {nnew.UserVoc.Level}
                                , {nnew.UserVoc.LevelCounter})";
                }

                insertUserVoc += values.Substring(1) + ";";
                await appDbContext.Database.ExecuteSqlRawAsync(insertUserVoc + userInfoUpdate);
                await appDbContext.SaveChangesAsync();
            }   
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Study
            if (vocMasterVM.Studys.Count > 0)
            {
                List<UserVoc> dbUservocs = new List<UserVoc>();
                Image dbImage = new Image();
                UserVoc dbUserVoc = new UserVoc();
                foreach (var study in vocMasterVM.Studys)
                {
                    dbUserVoc = await appDbContext.UserVocs.FirstOrDefaultAsync(x => x.UserId == vocMasterVM.UserInfo.UserId && x.VocId == study.UserVoc.VocId);
                    if (dbUserVoc != null)
                    {
                        dbUserVoc.Study = study.UserVoc.Study;
                        //dbUservocs.Add(dbUserVoc);
                    }
                    foreach (var img in study.Images)
                    {
                        dbImage = await appDbContext.Images.FirstOrDefaultAsync(x => x.Id == img.Id);
                        if (dbImage != null)
                        {
                            dbImage.Like = img.Like; dbImage.Dislike = img.Dislike;
                        }
                    }
                    List<Image> dbImages = appDbContext.Images.Where(x => x.VocId == study.Voc.Id).ToList();
                    var result = dbImages.Where(p => !study.Images.Any(p2 => p2.Uri == p.Uri));
                    appDbContext.Images.RemoveRange(result);
                    //for (int i = 0; i < dbImages.Count-1; i++)
                    //{
                    //    if (study.Images.Any(x => x.Id != dbImages[i].Id))
                    //    {
                    //        dbImages.RemoveAt(i);
                    //        i--;
                    //    }
                    //} 
                }
                await appDbContext.SaveChangesAsync();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Test
            if (vocMasterVM.Tests.Count > 0)
            {
                List<UserVoc> dbUservocs = new List<UserVoc>();
                UserVoc dbUserVoc = new UserVoc();
                foreach (var test in vocMasterVM.Tests)
                {
                    dbUserVoc = await appDbContext.UserVocs.FirstOrDefaultAsync(x => x.UserId == vocMasterVM.UserInfo.UserId && x.VocId == test.UserVoc.VocId);
                    dbUserVoc.Repetition = test.UserVoc.Repetition;
                    dbUserVoc.Success = test.UserVoc.Success;
                    dbUserVoc.NextReviewTime = test.UserVoc.NextReviewTime;
                    dbUserVoc.Study = test.UserVoc.Study;
                    dbUserVoc.LevelCounter = test.UserVoc.LevelCounter;
                    dbUserVoc.Level = test.UserVoc.Level;

                    dbUservocs.Add(dbUserVoc);
                }

                await appDbContext.SaveChangesAsync();
            }

            return vocMasterVM;
        }
        public async Task<VocVM> GetVocVMByText(VocVM vocVM)
        {
            vocVM.Voc = await appDbContext.Vocs
                .Include(x => x.VocAudios)
                .Include(x => x.Definitions).ThenInclude(x => x.Examples)
                .Include(x => x.Synonyms)
                .Include(x => x.Translates)
                .Include(x => x.Images)
                .Include(x => x.VocSubtitles).ThenInclude(x => x.Subtitle).ThenInclude(x => x.Movie)
                .Include(x => x.VocsQuotes).ThenInclude(x => x.Quote).ThenInclude(x => x.Influencer)
                .Include(x => x.VocsPhrases).ThenInclude(x => x.Phrase)
                .FirstOrDefaultAsync(x => x.Text == vocVM.Voc.Text);

            foreach (var vocPhrase in vocVM.Voc.VocsPhrases)
            {
                vocPhrase.Voc = null;
                vocPhrase.Phrase.VocsPhrases = null;
                vocPhrase.Phrase.Text = vocPhrase.Phrase.Text.Replace(vocVM.Voc.Text, $"<mark style=\"background-color: #FCF3CF;\">{vocVM.Voc.Text}</mark>");
                vocPhrase.Phrase.Text = vocPhrase.Phrase.Text.Replace(vocVM.Voc.Text.Substring(0, 1).ToUpper() + vocVM.Voc.Text.Substring(1), 
                    $"<mark style=\"background-color: #FCF3CF;\">{vocVM.Voc.Text.Substring(0, 1).ToUpper() + vocVM.Voc.Text.Substring(1)}</mark>");

            }
            return vocVM;
        }
    }
}
