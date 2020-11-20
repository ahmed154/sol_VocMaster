using pro_Models.Models;
using pro_Models.ViewModels;
using pro_Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public class SearchResultService : ISearchResultService
    {
        private string url = "api/SearchResult";
        private readonly IHttpService httpService;

        private JsonSerializerOptions defaultJsonSerializerOptions => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public SearchResultService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        #region Pro
        private async Task<T> DeserializeAsync<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseString, options);
        }
        #endregion

        public async Task<DictionaryApiDev> GetDictionaryApiDev(string word)
        {
            //List<DictionaryApiDev> dictionary = new List<DictionaryApiDev>();
            string pageContent;
            try
            {
                using (WebClient client = new WebClient())
                {
                    pageContent = client.DownloadString($"https://api.dictionaryapi.dev/api/v2/entries/en/{word}");
                    return System.Text.Json.JsonSerializer.Deserialize<List<DictionaryApiDev>>(pageContent)[0];
                }
            }
            catch (WebException wex)
            {
                if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    // error 404, do what you need to do
                    return new DictionaryApiDev();
                }
            }


            return null;
        }
        public async Task<List<Image>> GetImages(VocVM vocVM)
        {
            List<Image> images = new List<Image>();

            var response = await httpService.PostAsync($"{url}/GetImages", vocVM);

            if (response.Success)
            {
                images = await DeserializeAsync<List<Image>>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocVM.Exception = response.HttpResponseMessage.Content.ToString();
            }

            return images;
        }
        public async Task<List<Vid>> GetVids(VocVM vocVM)
        {
            List<Vid> vids = new List<Vid>();

            var response = await httpService.PostAsync($"{url}/GetVids", vocVM);

            if (response.Success)
            {
                vids = await DeserializeAsync<List<Vid>>(response.HttpResponseMessage, defaultJsonSerializerOptions);
            }
            else
            {
                vocVM.Exception = response.HttpResponseMessage.Content.ToString();
            }

            return vids;
        }
    }
}
