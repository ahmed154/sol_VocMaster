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
    public class LangService : ILangService
    {
        private readonly IHttpService httpService;
        private string url = "api/lang";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public LangService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<LangVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            LangVM langVM = new LangVM();
            if (httpResponseWrapper.Success)
            {
                langVM = await Deserialize<LangVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                langVM.Exception = await httpResponseWrapper.GetBody();
            }

            return langVM;
        }
        private async Task<LangVM> CheckDeserialize(HttpResponseWrapper<LangVM> httpResponseWrapper)
        {
            LangVM langVM = new LangVM();
            if (httpResponseWrapper.Success)
            {
                langVM = await Deserialize<LangVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                langVM.Exception = await httpResponseWrapper.GetBody();
            }

            return langVM;
        }
        private async Task<List<LangVM>> CheckDeserialize(HttpResponseWrapper<List<LangVM>> httpResponseWrapper)
        {
            List<LangVM> langVMs = new List<LangVM>();
            if (httpResponseWrapper.Success)
            {
                langVMs = await Deserialize<List<LangVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                langVMs.Add(new LangVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return langVMs;
        }
        #endregion

        public async Task<List<LangVM>> GetLangs()
        {
            var response = await httpService.Get<List<LangVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<LangVM> GetLang(int id)
        {
            var response = await httpService.Get<LangVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<LangVM> CreateLang(LangVM langVM)
        {
            var response = await httpService.PostAsync(url, langVM);
            return await CheckDeserialize(response);
        }
        public async Task<LangVM> UpdateLang(int id, LangVM langVM)
        {
            var response = await httpService.Put($"{url}/{id}", langVM);
            return await CheckDeserialize(response);
        }
        public async Task<LangVM> DeleteLang(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
