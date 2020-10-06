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
    public class PhraseService : IPhraseService
    {
        private readonly IHttpService httpService;
        private string url = "api/phrase";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public PhraseService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<PhraseVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            PhraseVM phraseVM = new PhraseVM();
            if (httpResponseWrapper.Success)
            {
                phraseVM = await Deserialize<PhraseVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                phraseVM.Exception = await httpResponseWrapper.GetBody();
            }

            return phraseVM;
        }
        private async Task<PhraseVM> CheckDeserialize(HttpResponseWrapper<PhraseVM> httpResponseWrapper)
        {
            PhraseVM phraseVM = new PhraseVM();
            if (httpResponseWrapper.Success)
            {
                phraseVM = await Deserialize<PhraseVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                phraseVM.Exception = await httpResponseWrapper.GetBody();
            }

            return phraseVM;
        }
        private async Task<List<PhraseVM>> CheckDeserialize(HttpResponseWrapper<List<PhraseVM>> httpResponseWrapper)
        {
            List<PhraseVM> phraseVMs = new List<PhraseVM>();
            if (httpResponseWrapper.Success)
            {
                phraseVMs = await Deserialize<List<PhraseVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                phraseVMs.Add(new PhraseVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return phraseVMs;
        }
        #endregion

        public async Task<List<PhraseVM>> GetPhrases()
        {
            var response = await httpService.Get<List<PhraseVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<PhraseVM> GetPhrase(int id)
        {
            var response = await httpService.Get<PhraseVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<PhraseVM> CreatePhrase(PhraseVM phraseVM)
        {
            var response = await httpService.PostAsync(url, phraseVM);
            return await CheckDeserialize(response);
        }
        public async Task<PhraseVM> UpdatePhrase(int id, PhraseVM phraseVM)
        {
            var response = await httpService.Put($"{url}/{id}", phraseVM);
            return await CheckDeserialize(response);
        }
        public async Task<PhraseVM> DeletePhrase(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }
    }
}
