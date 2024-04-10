using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using Site.Infrastructure.Helpers.HttpClients;

namespace Site.Application.Helpers.Https
{
    public partial class WebRequestHelper<TEntity> : IRequestHelper<TEntity>
    {
        private readonly HttpClient _client;
        public WebRequestHelper()
        {
            _client = new HttpClient();
        }

        public TEntity Get(string url)
        {
            var response = _client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public string Geturl(string url)
        {
            var response = _client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return response.RequestMessage.RequestUri.ToString();
            throw new IntegrationException(result);
        }

        public List<TEntity> GetCollection(string url)
        {
            var response = _client.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<TEntity>>(result);
            throw new IntegrationException(result);
        }

        public string Post(string url, TEntity entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = _client.PostAsync(url, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public TEntity Post(string url)
        {
            try
            {
                var response = _client.PostAsync(url, null).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TEntity>(result);

                throw new IntegrationException(result);
            }
            catch (AggregateException aggEx)
            {
                throw new IntegrationException(aggEx.InnerException.Message);
            }
        }

        public string Posts(string url, List<TEntity> entities)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json");
            var response = _client.PostAsync(url, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public TEntity Post(string baseUrl, string url, object additionalData)
        {
            _client.BaseAddress = new Uri(baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(additionalData), Encoding.UTF8, "application/json");
            var response = _client.PostAsync(url, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public string Put(string url, TEntity entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = _client.PutAsync(url, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public TEntity Put(string url)
        {
            var response = _client.PutAsync(url, null).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public string Put(string url, List<TEntity> entities)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json");
            var response = _client.PutAsync(url, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public TEntity Put(string baseUrl, string url, object additionalData)
        {
            _client.BaseAddress = new Uri(baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(additionalData), Encoding.UTF8, "application/json");
            var response = _client.PutAsync(url, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public string Delete(string url)
        {
            var response = _client.DeleteAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public TEntity Deleted(string url)
        {
            var response = _client.DeleteAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public string Deletes(string url, List<TEntity> entities)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json")
            };
            var response = _client.SendAsync(request).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public TEntity Delete(string baseUrl, string url, object additionalData)
        {
            _client.BaseAddress = new Uri(baseUrl);
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(additionalData), Encoding.UTF8, "application/json")
            };
            var response = _client.SendAsync(request).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public string Patch(string url, TEntity entity)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")
            };
            var response = _client.SendAsync(request).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public string Patch(string url, List<TEntity> entities)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json")
            };
            var response = _client.SendAsync(request).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public TEntity Patch(string baseUrl, string url, object additionalData)
        {
            _client.BaseAddress = new Uri(baseUrl);
            var request = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(additionalData), Encoding.UTF8, "application/json")
            };
            var response = _client.SendAsync(request).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public string Head(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, url);
            var response = _client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
                return response.Headers.ToString();
            throw new IntegrationException(response.Content.ReadAsStringAsync().Result);
        }

        public string Heads(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, url);
            var response = _client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;
            throw new IntegrationException(response.Content.ReadAsStringAsync().Result);
        }

    }

    public partial class WebRequestHelper<TEntity>
    {
        public async Task<TEntity> GetAsync(string url)
        {
            var response = await _client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public async Task<string> GeturlAsync(string url)
        {
            var response = await _client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return response.RequestMessage.RequestUri.ToString();
            throw new IntegrationException(result);
        }

        public async Task<List<TEntity>> GetCollectionAsync(string url)
        {
            var response = await _client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<TEntity>>(result);
            throw new IntegrationException(result);
        }

        public async Task<string> PostAsync(string url, TEntity entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public async Task<TEntity> PostAsync(string url)
        {
            try
            {
                var response = await _client.PostAsync(url, null);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<TEntity>(result);

                throw new IntegrationException(result);
            }
            catch (HttpRequestException httpRequestException)
            {
                throw new IntegrationException(httpRequestException.Message);
            }
            catch (Exception exception)
            {
                throw new IntegrationException(exception.Message);
            }
        }

        public async Task<string> PostsAsync(string url, List<TEntity> entities)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public async Task<TEntity> PostAsync(string baseUrl, string url, object additionalData)
        {
            _client.BaseAddress = new Uri(baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(additionalData), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public async Task<string> PutAsync(string url, TEntity entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public async Task<TEntity> PutAsync(string url)
        {
            var response = await _client.PutAsync(url, null);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public async Task<string> PutAsync(string url, List<TEntity> entities)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public async Task<TEntity> PutAsync(string baseUrl, string url, object additionalData)
        {
            _client.BaseAddress = new Uri(baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(additionalData), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public async Task<string> DeleteAsync(string url)
        {
            var response = await _client.DeleteAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public async Task<TEntity> DeletedAsync(string url)
        {
            var response = await _client.DeleteAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public async Task<string> DeletesAsync(string url, List<TEntity> entities)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public async Task<TEntity> DeleteAsync(string baseUrl, string url, object additionalData)
        {
            _client.BaseAddress = new Uri(baseUrl);
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(additionalData), Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public async Task<string> PatchAsync(string url, TEntity entity)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public async Task<string> PatchAsync(string url, List<TEntity> entities)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return result;
            throw new IntegrationException(result);
        }

        public async Task<TEntity> PatchAsync(string baseUrl, string url, object additionalData)
        {
            _client.BaseAddress = new Uri(baseUrl);
            var request = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(additionalData), Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TEntity>(result);
            throw new IntegrationException(result);
        }

        public async Task<string> HeadAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, url);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return response.Headers.ToString();
            throw new IntegrationException(response.Content);
        }

        public async Task<string> HeadsAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, url);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;
            throw new IntegrationException(response.Content);
        }

    }
}
