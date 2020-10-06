using pro_Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface ISearchResultService
    {
        Task<DictionaryApiDev> GetDictionaryApiDev(string word);
    }
}
