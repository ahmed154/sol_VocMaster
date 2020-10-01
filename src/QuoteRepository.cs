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
    public class QuoteRepository : IQuoteRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public QuoteRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<QuoteVM>> IQuoteRepository.Search(string name)
        {
            List<QuoteVM> quoteVMs = new List<QuoteVM>();

            IQueryable<Quote> query = appDbContext.Quotes;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Text.Contains(name));
            }

            var quotes = await query.ToListAsync();

            foreach (var quote in quotes)
            {
                quoteVMs.Add(new QuoteVM { Quote = quote });
            }
            return quoteVMs;
        }
        public async Task<List<QuoteVM>> GetQuotes()
        {
            List<QuoteVM> quoteVMs = new List<QuoteVM>();
            var quotes = await appDbContext.Quotes.ToListAsync();
            foreach (var quote in quotes)
            {
                quoteVMs.Add(new QuoteVM { Quote = quote});
            }
            return quoteVMs; 
        }
        public async Task<QuoteVM> GetQuote(int id)
        {
            QuoteVM quoteVM = new QuoteVM();
            quoteVM.Quote = await appDbContext.Quotes.FirstOrDefaultAsync(e => e.Id == id);
            return quoteVM;
        }
        public async Task<QuoteVM> CreateQuote(QuoteVM quoteVM)
        {
            var result = await appDbContext.Quotes.AddAsync(quoteVM.Quote);
            await appDbContext.SaveChangesAsync();

            quoteVM.Quote = result.Entity;
            return quoteVM;
        }
        public async Task<QuoteVM> UpdateQuote(QuoteVM quoteVM)
        {
            Quote result = await appDbContext.Quotes
                .FirstOrDefaultAsync(e => e.Id == quoteVM.Quote.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(quoteVM.Quote, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new QuoteVM { Quote = result };
            }

            return null;
        }
        public async Task<QuoteVM> DeleteQuote(int quoteId)
        {
            var result = await appDbContext.Quotes
                .FirstOrDefaultAsync(e => e.Id == quoteId);
            if (result != null)
            {
                appDbContext.Quotes.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new QuoteVM { Quote = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<Quote> GetQuoteByname(Quote quote)
        {
            return await appDbContext.Quotes.Where(n => n.Text == quote.Text && n.Id != quote.Id)
                .FirstOrDefaultAsync();
        }
    }
}
