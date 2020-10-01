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
    public class InfluencerService : IInfluencerService
    {
        private readonly IHttpService httpService;
        private string url = "api/influencer";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public InfluencerService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<InfluencerVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            InfluencerVM influencerVM = new InfluencerVM();
            if (httpResponseWrapper.Success)
            {
                influencerVM = await Deserialize<InfluencerVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                influencerVM.Exception = await httpResponseWrapper.GetBody();
            }

            return influencerVM;
        }
        private async Task<InfluencerVM> CheckDeserialize(HttpResponseWrapper<InfluencerVM> httpResponseWrapper)
        {
            InfluencerVM influencerVM = new InfluencerVM();
            if (httpResponseWrapper.Success)
            {
                influencerVM = await Deserialize<InfluencerVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                influencerVM.Exception = await httpResponseWrapper.GetBody();
            }

            return influencerVM;
        }
        private async Task<List<InfluencerVM>> CheckDeserialize(HttpResponseWrapper<List<InfluencerVM>> httpResponseWrapper)
        {
            List<InfluencerVM> influencerVMs = new List<InfluencerVM>();
            if (httpResponseWrapper.Success)
            {
                influencerVMs = await Deserialize<List<InfluencerVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                influencerVMs.Add(new InfluencerVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return influencerVMs;
        }
        #endregion

        public async Task<List<InfluencerVM>> GetInfluencers()
        {
            var response = await httpService.Get<List<InfluencerVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<InfluencerVM> GetInfluencer(int id)
        {
            var response = await httpService.Get<InfluencerVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<InfluencerVM> CreateInfluencer(InfluencerVM influencerVM)
        {
            var response = await httpService.PostAsync(url, influencerVM);
            return await CheckDeserialize(response);
        }
        public async Task<InfluencerVM> UpdateInfluencer(int id, InfluencerVM influencerVM)
        {
            var response = await httpService.Put($"{url}/{id}", influencerVM);
            return await CheckDeserialize(response);
        }
        public async Task<InfluencerVM> DeleteInfluencer(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
