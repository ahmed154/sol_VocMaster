using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IQuoteService
    {
        Task<List<QuoteVM>> GetQuotes();
        Task<QuoteVM> GetQuote(int id);
        Task<QuoteVM> UpdateQuote(int id, QuoteVM quoteVM);
        Task<QuoteVM> CreateQuote(QuoteVM quoteVM);
        Task<QuoteVM> DeleteQuote(int id);
    }
}
