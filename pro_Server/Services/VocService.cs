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
    public class VocService : IVocService
    {
        private readonly IHttpService httpService;
        private string url = "api/voc";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public VocService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<VocVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            VocVM vocVM = new VocVM();
            if (httpResponseWrapper.Success)
            {
                vocVM = await Deserialize<VocVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocVM.Exception = await httpResponseWrapper.GetBody();
            }

            return vocVM;
        }
        private async Task<VocVM> CheckDeserialize(HttpResponseWrapper<VocVM> httpResponseWrapper)
        {
            VocVM vocVM = new VocVM();
            if (httpResponseWrapper.Success)
            {
                vocVM = await Deserialize<VocVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocVM.Exception = await httpResponseWrapper.GetBody();
            }

            return vocVM;
        }
        private async Task<List<VocVM>> CheckDeserialize(HttpResponseWrapper<List<VocVM>> httpResponseWrapper)
        {
            List<VocVM> vocVMs = new List<VocVM>();
            if (httpResponseWrapper.Success)
            {
                vocVMs = await Deserialize<List<VocVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocVMs.Add(new VocVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return vocVMs;
        }
        #endregion

        public async Task<List<VocVM>> GetVocs()
        {
            var response = await httpService.Get<List<VocVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<VocVM> GetVocCheck(VocVM vocVM)
        {
            var response = await httpService.PostAsync($"{url}/check", vocVM);
            return await CheckDeserialize(response);
        }

        public async Task<VocVM> GetVoc(int id)
        {
            var response = await httpService.Get<VocVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<VocVM> GetVocVMByText(string txt)
        {
            var response = await httpService.PostAsync(url + "/GetVocVMByText", new VocVM { Voc = new Voc { Text = txt.Trim()} });
            return await CheckDeserialize(response);
        }
        public async Task<VocVM> CreateVoc(VocVM vocVM)
        {
            var response = await httpService.PostAsync(url, vocVM);
            return await CheckDeserialize(response);
        }
        public async Task<VocVM> UpdateVoc(int id, VocVM vocVM)
        {
            var response = await httpService.Put($"{url}/{id}", vocVM);
            return await CheckDeserialize(response);
        }
        public async Task<VocVM> DeleteVoc(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

        public async Task<List<VocCardVM>> GetControlVocCardVMs(VocCardVM vocCardVM)
        {
            List<VocCardVM> vocCardVMs = new List<VocCardVM>();
            var response = await httpService.PostAsync(url+ "/GetControlVocCardVMs", vocCardVM);

            if (response.Success)
            {
                vocCardVMs = await Deserialize<List<VocCardVM>>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocCardVMs.Add(new VocCardVM { Exception = await response.GetBody() });
            }

            return vocCardVMs;
        }
    }
}
