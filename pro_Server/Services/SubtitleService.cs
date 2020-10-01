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
    public class SubtitleService : ISubtitleService
    {
        private readonly IHttpService httpService;
        private string url = "api/subtitle";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public SubtitleService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<SubtitleVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            SubtitleVM subtitleVM = new SubtitleVM();
            if (httpResponseWrapper.Success)
            {
                subtitleVM = await Deserialize<SubtitleVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                subtitleVM.Exception = await httpResponseWrapper.GetBody();
            }

            return subtitleVM;
        }
        private async Task<SubtitleVM> CheckDeserialize(HttpResponseWrapper<SubtitleVM> httpResponseWrapper)
        {
            SubtitleVM subtitleVM = new SubtitleVM();
            if (httpResponseWrapper.Success)
            {
                subtitleVM = await Deserialize<SubtitleVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                subtitleVM.Exception = await httpResponseWrapper.GetBody();
            }

            return subtitleVM;
        }
        private async Task<List<SubtitleVM>> CheckDeserialize(HttpResponseWrapper<List<SubtitleVM>> httpResponseWrapper)
        {
            List<SubtitleVM> subtitleVMs = new List<SubtitleVM>();
            if (httpResponseWrapper.Success)
            {
                subtitleVMs = await Deserialize<List<SubtitleVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                subtitleVMs.Add(new SubtitleVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return subtitleVMs;
        }
        #endregion

        public async Task<List<SubtitleVM>> GetSubtitles()
        {
            var response = await httpService.Get<List<SubtitleVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<SubtitleVM> GetSubtitle(int id)
        {
            var response = await httpService.Get<SubtitleVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<SubtitleVM> CreateSubtitle(SubtitleVM subtitleVM)
        {
            var response = await httpService.PostAsync(url, subtitleVM);
            return await CheckDeserialize(response);
        }
        public async Task<SubtitleVM> UpdateSubtitle(int id, SubtitleVM subtitleVM)
        {
            var response = await httpService.Put($"{url}/{id}", subtitleVM);
            return await CheckDeserialize(response);
        }
        public async Task<SubtitleVM> DeleteSubtitle(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

        public async Task<List<SubtitleVM>> CalcSubtitlesVM(Voc voc)
        {
            List<SubtitleVM> subtitlesVM = new List<SubtitleVM>();
            var response = await httpService.PostAsync($"{url}/CalcSubtitlesVM", voc);

            if (response.Success)
            {
                try
                {
                    subtitlesVM = await Deserialize<List<SubtitleVM>>(response.HttpResponseMessage, defaultJsonSerializerOptions);
                }
                catch (Exception ex)
                {
                    subtitlesVM.Add(new SubtitleVM { Exception = ex.Message });
                }
            }
            else
            {
                subtitlesVM.Add(new SubtitleVM { Exception = await response.GetBody() });
            }

            return subtitlesVM;
        }
    }
}
