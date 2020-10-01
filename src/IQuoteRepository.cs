using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IQuoteRepository
    {
        Task<List<QuoteVM>> Search(string name);
        Task<List<QuoteVM>> GetQuotes();
        Task<QuoteVM> GetQuote(int quoteId);
        Task<QuoteVM> CreateQuote(QuoteVM quoteVM);
        Task<QuoteVM> UpdateQuote(QuoteVM quote);
        Task<QuoteVM> DeleteQuote(int quoteId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Quote> GetQuoteByName(string name);
        Task<Quote> GetQuoteByname(Quote quote);
    }
}
