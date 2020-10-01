using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using pro_Models.Models;
using pro_Models.ViewModels;
using pro_Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IHttpService httpService;
        private string url = "api/quote";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public QuoteService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<QuoteVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            QuoteVM quoteVM = new QuoteVM();
            if (httpResponseWrapper.Success)
            {
                quoteVM = await Deserialize<QuoteVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                quoteVM.Exception = await httpResponseWrapper.GetBody();
            }

            return quoteVM;
        }
        private async Task<QuoteVM> CheckDeserialize(HttpResponseWrapper<QuoteVM> httpResponseWrapper)
        {
            QuoteVM quoteVM = new QuoteVM();
            if (httpResponseWrapper.Success)
            {
                quoteVM = await Deserialize<QuoteVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                quoteVM.Exception = await httpResponseWrapper.GetBody();
            }

            return quoteVM;
        }
        private async Task<List<QuoteVM>> CheckDeserialize(HttpResponseWrapper<List<QuoteVM>> httpResponseWrapper)
        {
            List<QuoteVM> quoteVMs = new List<QuoteVM>();
            if (httpResponseWrapper.Success)
            {
                quoteVMs = await Deserialize<List<QuoteVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                quoteVMs.Add(new QuoteVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return quoteVMs;
        }
        #endregion

        public async Task<List<QuoteVM>> GetQuotes()
        {
            var response = await httpService.Get<List<QuoteVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<QuoteVM> GetQuote(int id)
        {
            var response = await httpService.Get<QuoteVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<QuoteVM> CreateQuote(QuoteVM quoteVM)
        {
            var response = await httpService.PostAsync(url, quoteVM);
            return await CheckDeserialize(response);
        }
        public async Task<QuoteVM> UpdateQuote(int id, QuoteVM quoteVM)
        {
            var response = await httpService.Put($"{url}/{id}", quoteVM);
            return await CheckDeserialize(response);
        }
        public async Task<QuoteVM> DeleteQuote(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
