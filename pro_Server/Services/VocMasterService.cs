using pro_Models.ViewModels;
using pro_Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public class VocMasterService : IVocMasterService
    {
        private readonly IHttpService httpService;
        private string url = "api/VocMaster";

        private JsonSerializerOptions defaultJsonSerializerOptions => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public VocMasterService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> DeserializeAsync<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<VocMasterVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            VocMasterVM vocMasterVM = new VocMasterVM();
            if (httpResponseWrapper.Success)
            {
                vocMasterVM = await DeserializeAsync<VocMasterVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocMasterVM.Exception = await httpResponseWrapper.GetBody();
            }

            return vocMasterVM;
        }
        #endregion

        public async Task<VocMasterVM> GetVocMasterVM(UserNameVM userNameVM)
        {
            var response = await httpService.PostAsync($"{url}/GetVocMasterVM", userNameVM);

            VocMasterVM vocMasterVM = new VocMasterVM();
            if (response.Success)
            {
                vocMasterVM = await DeserializeAsync<VocMasterVM>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocMasterVM.Exception = response.HttpResponseMessage.Content.ToString();
            }

            return vocMasterVM;
        }
        public async Task<VocMasterVM> UpdateVocMasterVM(VocMasterVM vocMasterVM)
        {
            var response = await httpService.Put<VocMasterVM>($"{url}/UpdateVocMasterVM", vocMasterVM);

            if (response.Success)
            {
                vocMasterVM = await DeserializeAsync<VocMasterVM>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocMasterVM.Exception = response.HttpResponseMessage.Content.ToString();
            }

            return vocMasterVM;
        }
        public async Task<VocVM> GetVocVMByText(VocVM vocVM)
        {
            var response = await httpService.PostAsync($"{url}/GetVocVMByText", vocVM);

            if (response.Success)
            {
                vocVM = await DeserializeAsync<VocVM>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocVM.Exception = response.HttpResponseMessage.Content.ToString();
            }

            return vocVM;
        }
        public async Task<List<pro_Models.Models.Image>> GetImagesByText(VocVM vocVM)
        {
            List<pro_Models.Models.Image> images = new List<pro_Models.Models.Image>();

            var response = await httpService.PostAsync($"{url}/GetImagesByText", vocVM);

            if (response.Success)
            {
                images = await DeserializeAsync<List<pro_Models.Models.Image>>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocVM.Exception = response.HttpResponseMessage.Content.ToString();
            }

            return images;
        }
        public async Task<List<pro_Models.Models.VocSubtitle>> GetVocSubtitlesByText(VocVM vocVM)
        {
            List<pro_Models.Models.VocSubtitle> vocSubtitles = new List<pro_Models.Models.VocSubtitle>();

            var response = await httpService.PostAsync($"{url}/GetVocSubtitlesByText", vocVM);

            if (response.Success)
            {
                vocSubtitles = await DeserializeAsync<List<pro_Models.Models.VocSubtitle>>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocVM.Exception = response.HttpResponseMessage.Content.ToString();
            }

            return vocSubtitles;
        }
    }
}
