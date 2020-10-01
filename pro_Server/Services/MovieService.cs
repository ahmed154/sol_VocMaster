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
    public class MovieService : IMovieService
    {
        private readonly IHttpService httpService;
        private string url = "api/movie";
        private JsonSerializerOptions defaultJsonSerializerOptions =>new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public MovieService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        private async Task<MovieVM> CheckDeserialize(HttpResponseWrapper<object> httpResponseWrapper)
        {
            MovieVM movieVM = new MovieVM();
            if (httpResponseWrapper.Success)
            {
                movieVM = await Deserialize<MovieVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                movieVM.Exception = await httpResponseWrapper.GetBody();
            }

            return movieVM;
        }
        private async Task<MovieVM> CheckDeserialize(HttpResponseWrapper<MovieVM> httpResponseWrapper)
        {
            MovieVM movieVM = new MovieVM();
            if (httpResponseWrapper.Success)
            {
                movieVM = await Deserialize<MovieVM>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                movieVM.Exception = await httpResponseWrapper.GetBody();
            }

            return movieVM;
        }
        private async Task<List<MovieVM>> CheckDeserialize(HttpResponseWrapper<List<MovieVM>> httpResponseWrapper)
        {
            List<MovieVM> movieVMs = new List<MovieVM>();
            if (httpResponseWrapper.Success)
            {
                movieVMs = await Deserialize<List<MovieVM>>(httpResponseWrapper.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                movieVMs.Add(new MovieVM { Exception = httpResponseWrapper.HttpResponseMessage.Content.ToString() });
            }

            return movieVMs;
        }
        #endregion

        public async Task<List<MovieVM>> GetMovies()
        {
            var response = await httpService.Get<List<MovieVM>>(url);
            return await CheckDeserialize(response);
        }
        public async Task<MovieVM> GetMovie(int id)
        {
            var response = await httpService.Get<MovieVM>($"{url}/{id}");
            return await CheckDeserialize(response);
        }
        public async Task<MovieVM> CreateMovie(MovieVM movieVM)
        {
            var response = await httpService.PostAsync(url, movieVM);
            return await CheckDeserialize(response);
        }
        public async Task<MovieVM> UpdateMovie(int id, MovieVM movieVM)
        {
            var response = await httpService.Put($"{url}/{id}", movieVM);
            return await CheckDeserialize(response);
        }
        public async Task<MovieVM> DeleteMovie(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            return await CheckDeserialize(response);
        }

    }
}
