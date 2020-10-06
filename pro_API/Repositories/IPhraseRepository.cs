using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IPhraseRepository
    {
        Task<List<PhraseVM>> Search(string name);
        Task<List<PhraseVM>> GetPhrases();
        Task<PhraseVM> GetPhrase(int phraseId);
        Task<PhraseVM> CreatePhrase(PhraseVM phraseVM);
        Task<PhraseVM> UpdatePhrase(PhraseVM phrase);
        Task<PhraseVM> DeletePhrase(int phraseId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Phrase> GetPhraseByName(string name);
        Task<Phrase> GetPhraseByname(Phrase phrase);
    }
}
