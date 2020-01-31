using IdentityModel.Client;
using IGRMgr.Frontend.Contracts;
using IGRMgr.Frontend.Contracts.Repository;
using IGRMgr.Frontend.Exceptions;
using IGRMgr.Frontend.Utility;
using Newtonsoft.Json;
using NToastNotify;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Frontend.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ApiHttpClient _apiClient;
        private readonly IAccessTokenComponent _tokenComponent;
        private readonly IToastNotification _toastNotification;
        public GenericRepository(ApiHttpClient apiClient, 
            IAccessTokenComponent tokenComponent,
            IToastNotification toastNotification)
        { 
            _apiClient = apiClient;
            _tokenComponent = tokenComponent;
            _toastNotification = toastNotification;
        }
        public async Task DeleteAsync(string uri)
        {
            var accessToken = await _tokenComponent.GetTokenAsync();
            _apiClient.Client.SetBearerToken(accessToken);

            await _apiClient.Client.DeleteAsync(uri);
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var accessToken = await _tokenComponent.GetTokenAsync();
                _apiClient.Client.SetBearerToken(accessToken);

                string jsonResult = string.Empty;

                var responseMessage = await Policy.Handle<WebException>(ex =>
                {
                    _toastNotification.AddErrorToastMessage($"{ex.GetType().Name + " : " + ex.Message}");
                    return true;
                })
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () => await _apiClient.Client.GetAsync(uri));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var obj = JsonConvert.DeserializeObject<T>(jsonResult);
                    return obj;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
            }
            catch(Exception e)
            {
                _toastNotification.AddErrorToastMessage($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<T> PostAsync<T>(string uri, T data)
        {
            try
            {
                var accessToken = await _tokenComponent.GetTokenAsync();
                _apiClient.Client.SetBearerToken(accessToken);

                string jsonResult = string.Empty;

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        _toastNotification.AddErrorToastMessage($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await _apiClient.Client.PostAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<TR> PostAsync<T, TR>(string uri, T data)
        {
            try
            {
                var accessToken = await _tokenComponent.GetTokenAsync();
                _apiClient.Client.SetBearerToken(accessToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        _toastNotification.AddErrorToastMessage($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await _apiClient.Client.PostAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<T> PutAsync<T>(string uri, T data)
        {
            try
            {
                var accessToken = await _tokenComponent.GetTokenAsync();
                _apiClient.Client.SetBearerToken(accessToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        _toastNotification.AddErrorToastMessage($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await _apiClient.Client.PutAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }
    }
}
