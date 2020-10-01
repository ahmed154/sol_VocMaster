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
    public class IdiomService : IIdiomService
    {
        private readonly IHttpService httpService;
        private string url = "api/idiom";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public IdiomService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<IdiomVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            IdiomVM idiomVM = new IdiomVM();
            if (httpResponseWrapper.Success)
            {
                idiomVM = await Deserialize<IdiomVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                idiomVM.Exception = await httpResponseWrapper.GetBody();
            }

            return idiomVM;
        }
        private async Task<IdiomVM> CheckDeserialize(HttpResponseWrapper<IdiomVM> httpResponseWrapper)
        {
            IdiomVM idiomVM = new IdiomVM();
            if (httpResponseWrapper.Success)
            {
                idiomVM = await Deserialize<IdiomVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                idiomVM.Exception = await httpResponseWrapper.GetBody();
            }

            return idiomVM;
        }
        private async Task<List<IdiomVM>> CheckDeserialize(HttpResponseWrapper<List<IdiomVM>> httpResponseWrapper)
        {
            List<IdiomVM> idiomVMs = new List<IdiomVM>();
            if (httpResponseWrapper.Success)
            {
                idiomVMs = await Deserialize<List<IdiomVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                idiomVMs.Add(new IdiomVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return idiomVMs;
        }
        #endregion

        public async Task<List<IdiomVM>> GetIdioms()
        {
            var response = await httpService.Get<List<IdiomVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<IdiomVM> GetIdiom(int id)
        {
            var response = await httpService.Get<IdiomVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<IdiomVM> CreateIdiom(IdiomVM idiomVM)
        {
            var response = await httpService.PostAsync(url, idiomVM);
            return await CheckDeserialize(response);
        }
        public async Task<IdiomVM> UpdateIdiom(int id, IdiomVM idiomVM)
        {
            var response = await httpService.Put($"{url}/{id}", idiomVM);
            return await CheckDeserialize(response);
        }
        public async Task<IdiomVM> DeleteIdiom(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
