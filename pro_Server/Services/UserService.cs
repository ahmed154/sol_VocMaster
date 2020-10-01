﻿using Newtonsoft.Json;
using pro_Models.Models;
using System.Net.Http;
using System.Threading.Tasks;
using pro_Server.Services;

namespace BlazorServerApp.Services
{
    public class UserService : IUserService
    {
        public HttpClient _httpClient { get; }
        //public AppSettings _appSettings { get; }

        public UserService(HttpClient httpClient)
        {
            //_appSettings = appSettings.Value;

            //httpClient.BaseAddress = new Uri(_appSettings.BookStoresBaseAddress);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");

            _httpClient = httpClient;
        }

        public async Task<User> LoginAsync(User user)
        {
            //user.Password = Utility.Encrypt(user.Password);
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/accounts/Login");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = new User();
            if (responseStatusCode == System.Net.HttpStatusCode.OK)
            {
                returnedUser = JsonConvert.DeserializeObject<User>(responseBody);
            }

            return await Task.FromResult(returnedUser);
        }
        public async Task<User> RegisterUserAsync(User user)
        {
            //user.Password = Utility.Encrypt(user.Password);
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/accounts/create");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = new User();
            if (responseStatusCode == System.Net.HttpStatusCode.OK)
            {
                returnedUser = JsonConvert.DeserializeObject<User>(responseBody);
            }

            return await Task.FromResult(returnedUser);
        }
        public async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            string serializedRefreshRequest = JsonConvert.SerializeObject(accessToken);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/GetUserByAccessToken");
            requestMessage.Content = new StringContent(serializedRefreshRequest);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        //public async Task<User> RefreshTokenAsync(RefreshRequest refreshRequest)
        //{
        //    string serializedUser = JsonConvert.SerializeObject(refreshRequest);

        //    var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/RefreshToken");
        //    requestMessage.Content = new StringContent(serializedUser);

        //    requestMessage.Content.Headers.ContentType
        //        = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        //    var response = await _httpClient.SendAsync(requestMessage);

        //    var responseStatusCode = response.StatusCode;
        //    var responseBody = await response.Content.ReadAsStringAsync();

        //    var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

        //    return await Task.FromResult(returnedUser);
        //}
    }
}