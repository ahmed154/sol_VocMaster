using pro_Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public class SearchResultService : ISearchResultService
    {
        private JsonSerializerOptions defaultJsonSerializerOptions => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public SearchResultService()
        {

        }

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
                }
            }


            return null;
        }
    }
}
