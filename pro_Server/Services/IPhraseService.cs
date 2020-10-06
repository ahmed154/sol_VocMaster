using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IPhraseService
    {
        Task<List<PhraseVM>> GetPhrases();
        Task<PhraseVM> GetPhrase(int id);
        Task<PhraseVM> UpdatePhrase(int id, PhraseVM phraseVM);
        Task<PhraseVM> CreatePhrase(PhraseVM phraseVM);
        Task<PhraseVM> DeletePhrase(int id);
    }
}
