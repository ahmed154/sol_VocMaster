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
    public class ImageService : IImageService
    {
        private readonly IHttpService httpService;
        private string url = "api/image";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public ImageService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<ImageVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            ImageVM imageVM = new ImageVM();
            if (httpResponseWrapper.Success)
            {
                imageVM = await Deserialize<ImageVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                imageVM.Exception = await httpResponseWrapper.GetBody();
            }

            return imageVM;
        }
        private async Task<ImageVM> CheckDeserialize(HttpResponseWrapper<ImageVM> httpResponseWrapper)
        {
            ImageVM imageVM = new ImageVM();
            if (httpResponseWrapper.Success)
            {
                imageVM = await Deserialize<ImageVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                imageVM.Exception = await httpResponseWrapper.GetBody();
            }

            return imageVM;
        }
        private async Task<List<ImageVM>> CheckDeserialize(HttpResponseWrapper<List<ImageVM>> httpResponseWrapper)
        {
            List<ImageVM> imageVMs = new List<ImageVM>();
            if (httpResponseWrapper.Success)
            {
                imageVMs = await Deserialize<List<ImageVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                imageVMs.Add(new ImageVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return imageVMs;
        }
        #endregion

        public async Task<List<ImageVM>> GetImages()
        {
            var response = await httpService.Get<List<ImageVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<ImageVM> GetImage(int id)
        {
            var response = await httpService.Get<ImageVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<ImageVM> CreateImage(ImageVM imageVM)
        {
            var response = await httpService.PostAsync(url, imageVM);
            return await CheckDeserialize(response);
        }
        public async Task<ImageVM> UpdateImage(int id, ImageVM imageVM)
        {
            var response = await httpService.Put($"{url}/{id}", imageVM);
            return await CheckDeserialize(response);
        }
        public async Task<ImageVM> DeleteImage(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
