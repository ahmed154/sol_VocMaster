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
    public class UserVocService : IUserVocService
    {
        private readonly IHttpService httpService;
        private string url = "api/uservoc";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public UserVocService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private T Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString.Result, options);
        }
        private async Task<T> DeserializeAsync<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<UserVocVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            UserVocVM uservocVM = new UserVocVM();
            if (httpResponseWrapper.Success)
            {
                uservocVM = await DeserializeAsync<UserVocVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                uservocVM.Exception = await httpResponseWrapper.GetBody();
            }

            return uservocVM;
        }
        private async Task<UserVocVM> CheckDeserialize(HttpResponseWrapper<UserVocVM> httpResponseWrapper)
        {
            UserVocVM uservocVM = new UserVocVM();
            if (httpResponseWrapper.Success)
            {
                uservocVM = await DeserializeAsync<UserVocVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                uservocVM.Exception = await httpResponseWrapper.GetBody();
            }

            return uservocVM;
        }
        private async Task<List<UserVocVM>> CheckDeserialize(HttpResponseWrapper<List<UserVocVM>> httpResponseWrapper)
        {
            List<UserVocVM> uservocVMs = new List<UserVocVM>();
            if (httpResponseWrapper.Success)
            {
                uservocVMs = await DeserializeAsync<List<UserVocVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                uservocVMs.Add(new UserVocVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return uservocVMs;
        }

        #endregion

        public async Task<List<UserVocVM>> GetVocStudy(UserVocVM userVocVM)
        {
            var response = await httpService.PostAsync($"{url}/study", userVocVM);

            List<UserVocVM> userVocVMs = new List<UserVocVM>();
            if (response.Success)
            {
                userVocVMs = await DeserializeAsync<List<UserVocVM>>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                userVocVMs.Add(new UserVocVM { Exception = response.HttpResponseMessage.Content.ToString() });
            }

            return userVocVMs;
        }
        public async Task<List<UserVocVM>> GetVocCheck(UserVocVM userVocVM)
        {
            var response = await httpService.PostAsync($"{url}/check", userVocVM);

            List<UserVocVM> userVocVMs = new List<UserVocVM>();
            if (response.Success)
            {
                userVocVMs = await DeserializeAsync<List<UserVocVM>>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                userVocVMs.Add(new UserVocVM { Exception = response.HttpResponseMessage.Content.ToString() });
            }

            return userVocVMs;
        }
        public async Task<List<UserVocVM>> GetUserVocs()
        {
            var response = await httpService.Get<List<UserVocVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<UserVocVM> GetUserVoc(int id)
        {
            var response = await httpService.Get<UserVocVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<UserVocListVM> GetStudyVM(UserVocListVM studyVM)
        {
            var response = await httpService.PostAsync($"{url}/GetStudyVM", studyVM);

            if (response.Success)
            {
                studyVM = await DeserializeAsync<UserVocListVM>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                studyVM.Exception = await response.GetBody();
            }

            return studyVM;
        }
        public async Task<UserVocListVM> GetReviewVM(UserVocListVM reviewVM)
        {
            var response = await httpService.PostAsync($"{url}/GetReviewVM", reviewVM);

            if (response.Success)
            {
                reviewVM = await DeserializeAsync<UserVocListVM>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                reviewVM.Exception = await response.GetBody();
            }

            return reviewVM;
        }
        public async Task<UserVocVM> CreateUserVoc(UserVocVM uservocVM)
        {
            var response = await httpService.PostAsync(url, uservocVM);
            return await CheckDeserialize(response);
        }
        public async Task CreateUserVocs(UserVocListVM userVocVMsCreate)
        {
            var response = await httpService.PostAsync($"{url}/list", userVocVMsCreate);
        }
        public async Task<UserVocVM> UpdateUserVoc(int id, UserVocVM uservocVM)
        {
            var response = await httpService.Put($"{url}/{id}", uservocVM);
            return await CheckDeserialize(response);
        }
        public async Task UpdateUserVocList(UserVocListVM userVocVMsUpdate)
        {
            await httpService.Put($"{url}/list", userVocVMsUpdate);
        }
        public async Task UpdateUserVocsReview(UserVocListVM userVocVMsUpdate)
        {
            await httpService.Put($"{url}/review", userVocVMsUpdate);
        }
        public async Task<UserVocVM> DeleteUserVoc(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

        public async Task<VocCardVM> AddVocCardVM(VocCardVM vocCardVM)
        {
            var response = await httpService.PostAsync($"{url}/AddVocCardVM", vocCardVM);

            if (response.Success)
            {
                vocCardVM = await DeserializeAsync<VocCardVM>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocCardVM.Exception = await response.GetBody();
            }

            return vocCardVM;
        }
        public List<VocCardVM> GetVocCardVMs(UserNameVM userNameVM)
        {
            var response =  httpService.Post($"{url}/GetVocCardVMs", userNameVM);

            List<VocCardVM> VocCardVMs = new List<VocCardVM>();
            //if (response.Success)
            //{
                VocCardVMs =  Deserialize<List<VocCardVM>>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            //}
            //else
            //{
                //VocCardVMs.Add(new VocCardVM { Exception = response.HttpResponseMessage.Content.ToString() });
            //}

            return VocCardVMs;
        }

        public async Task<UserVocListVM> GetUserVocVMsResults(UserNameVM userNameVM)
        {
            var response = httpService.Post($"{url}/GetUserVocVMsResults", userNameVM);

            UserVocListVM userVocListVMs = new UserVocListVM();

            userVocListVMs = await DeserializeAsync<UserVocListVM>(response.HttpResponseMessage, defaultJsonSerializerOptions);


            return userVocListVMs;
        }

    }
}
