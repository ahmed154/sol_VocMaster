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

namespace pro_API.Repositories
{
    public class PhraseRepository : IPhraseRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public PhraseRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<PhraseVM>> IPhraseRepository.Search(string name)
        {
            List<PhraseVM> phraseVMs = new List<PhraseVM>();

            IQueryable<Phrase> query = appDbContext.Phrases;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Text.Contains(name));
            }

            var phrases = await query.ToListAsync();

            foreach (var phrase in phrases)
            {
                phraseVMs.Add(new PhraseVM { Phrase = phrase });
            }
            return phraseVMs;
        }
        public async Task<List<PhraseVM>> GetPhrases()
        {
            //List<Phrase> Phrases = appDbContext.Phrases.ToList();
            //foreach (Phrase phrase in Phrases)
            //{
            //    foreach (string item in phrase.Text.Split(' '))
            //    {
            //        try
            //        {
            //            Voc voc = await appDbContext.Vocs.FirstOrDefaultAsync(x => x.Text == item);

            //            if (voc != null)
            //            {
            //                if (voc.VocsPhrases == null) voc.VocsPhrases = new List<VocsPhrases>();
            //                voc.VocsPhrases.Add(new VocsPhrases { PhraseId = phrase.Id });
            //            }

            //        }
            //        catch (Exception ex)
            //        {

            //            throw;
            //        }

            //    }
            //}
            //await appDbContext.SaveChangesAsync();


            List<PhraseVM> phraseVMs = new List<PhraseVM>();
            var phrases = await appDbContext.Phrases.ToListAsync();
            foreach (var phrase in phrases)
            {
                phraseVMs.Add(new PhraseVM { Phrase = phrase});
            }
            return phraseVMs; 
        }
        public async Task<PhraseVM> GetPhrase(int id)
        {
            PhraseVM phraseVM = new PhraseVM();
            phraseVM.Phrase = await appDbContext.Phrases.FirstOrDefaultAsync(e => e.Id == id);
            return phraseVM;
        }
        public async Task<PhraseVM> CreatePhrase(PhraseVM phraseVM)
        {
            var result = await appDbContext.Phrases.AddAsync(phraseVM.Phrase);

            foreach (string item in phraseVM.Phrase.Text.Split(' '))
            {     
                Voc voc = await appDbContext.Vocs.FirstOrDefaultAsync(x => x.Text == item);

                if (voc != null)
                {
                    if(phraseVM.Phrase.VocsPhrases == null) phraseVM.Phrase.VocsPhrases = new List<VocsPhrases>();
                    phraseVM.Phrase.VocsPhrases.Add(new VocsPhrases { VocId = voc.Id });
                }
            }

            await appDbContext.SaveChangesAsync();

            result.Entity.VocsPhrases = null;
            phraseVM.Phrase = result.Entity;
            return phraseVM;
        }
        public async Task<PhraseVM> UpdatePhrase(PhraseVM phraseVM)
        {
            Phrase result = await appDbContext.Phrases
                .FirstOrDefaultAsync(e => e.Id == phraseVM.Phrase.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(phraseVM.Phrase, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new PhraseVM { Phrase = result };
            }

            return null;
        }
        public async Task<PhraseVM> DeletePhrase(int phraseId)
        {
            var result = await appDbContext.Phrases
                .FirstOrDefaultAsync(e => e.Id == phraseId);
            if (result != null)
            {
                appDbContext.Phrases.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new PhraseVM { Phrase = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<Phrase> GetPhraseByname(Phrase phrase)
        {
            return await appDbContext.Phrases.Where(n => n.Text == phrase.Text && n.Id != phrase.Id)
                .FirstOrDefaultAsync();
        }

        public async Task SetPhrases()
        {
            foreach (Phrase phrase in appDbContext.Phrases)
            {
                foreach (string item in phrase.Text.Split(' '))
                {
                    Voc voc = await appDbContext.Vocs.FirstOrDefaultAsync(x => x.Text == item);
                    if(voc != null)
                    {
                        voc.VocsPhrases.Add(new VocsPhrases { PhraseId = phrase.Id});
                    }
                }
            }
        }
    }
}
