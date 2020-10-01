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
    public class PickNewService : IPickNewService
    {
        private readonly IHttpService httpService;
        private string url = "api/picknew";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public PickNewService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> DeserializeAsync<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<PickNewVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            PickNewVM picknewVM = new PickNewVM();
            if (httpResponseWrapper.Success)
            {
                picknewVM = await DeserializeAsync<PickNewVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                picknewVM.Exception = await httpResponseWrapper.GetBody();
            }

            return picknewVM;
        }
        private async Task<PickNewVM> CheckDeserialize(HttpResponseWrapper<PickNewVM> httpResponseWrapper)
        {
            PickNewVM picknewVM = new PickNewVM();
            if (httpResponseWrapper.Success)
            {
                picknewVM = await DeserializeAsync<PickNewVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                picknewVM.Exception = await httpResponseWrapper.GetBody();
            }

            return picknewVM;
        }
        #endregion

        public async Task<PickNewVM> GetPickNew(PickNewVM pickNewVM)
        {
            var response = await httpService.PostAsync<PickNewVM>(url, pickNewVM);
            return await CheckDeserialize(response);
        }
        public async Task<List<VocCardVM>> GetVocCardVMs(UserNameVM userNameVM)
        {
            var response = await httpService.PostAsync<UserNameVM>(url, userNameVM);

            List<VocCardVM> vocCardVMs = new List<VocCardVM>();
            vocCardVMs = await DeserializeAsync<List<VocCardVM>>(response.HttpResponseMessage, defaultJsonSerializerOptions);

            return vocCardVMs;
        }
    }
}
