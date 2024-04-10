using RestSharp;
using Site.Application.Helpers.Https;
using Site.Infrastructure.Helpers.RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Helpers.RestSharp
{
    public partial class RestRequestHelper<TEntity> : IRestClient<TEntity> where TEntity : class, new()
    {
        private readonly RestClient _client;
        public RestRequestHelper(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public TEntity Get(string url)
        {
            var request = new RestRequest(url, Method.Get);
            var response = _client.Execute<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public string Geturl(string url)
        {
            var request = new RestRequest(url, Method.Get);
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.ResponseUri.ToString();
            throw new IntegrationException(response.Content);
        }

        public List<TEntity> GetCollection(string url)
        {
            var request = new RestRequest(url, Method.Get);
            var response = _client.Execute<List<TEntity>>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public TEntity Post(string url)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody(new TEntity());
            var response = _client.Execute<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public string Post(string url, TEntity entity)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody(JsonConvert.SerializeObject(entity));
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public string Posts(string url, List<TEntity> entity)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody(JsonConvert.SerializeObject(entity));
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public TEntity Post(string baseUrl, string url, object entities)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = client.Execute<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public string Put(string url, TEntity entity)
        {
            var request = new RestRequest(url, Method.Put);
            request.AddJsonBody(JsonConvert.SerializeObject(entity));
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public TEntity Put(string url)
        {
            var request = new RestRequest(url, Method.Put);
            var response = _client.Execute<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public string Put(string url, List<TEntity> entities)
        {
            var request = new RestRequest(url, Method.Put);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public TEntity Put(string baseUrl, string url, object entities)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Put);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = client.Execute<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public string Delete(string url)
        {
            var request = new RestRequest(url, Method.Delete);
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public TEntity Deleted(string url)
        {
            var request = new RestRequest(url, Method.Delete);
            var response = _client.Execute<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public string Deletes(string url, List<TEntity> entities)
        {
            var request = new RestRequest(url, Method.Delete);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public TEntity Delete(string baseUrl, string url, object entities)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Delete);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = client.Execute<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public string Patch(string url, TEntity entity)
        {
            var request = new RestRequest(url, Method.Patch);
            request.AddJsonBody(JsonConvert.SerializeObject(entity));
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public string Patch(string url, List<TEntity> entities)
        {
            var request = new RestRequest(url, Method.Patch);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public TEntity Patch(string baseUrl, string url, object entities)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Patch);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = client.Execute<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public string Head(string url)
        {
            var request = new RestRequest(url, Method.Head);
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Headers.ToString();
            throw new IntegrationException(response.Content);
        }

        public string Heads(string url)
        {
            var request = new RestRequest(url, Method.Head);
            var response = _client.Execute(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

    }

    public partial class RestRequestHelper<TEntity> 
    {
        public async Task<TEntity> GetAsync(string url)
        {
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public async Task<string> GeturlAsync(string url)
        {
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.ResponseUri.ToString();
            throw new IntegrationException(response.Content);
        }

        public async Task<List<TEntity>> GetCollectionAsync(string url)
        {
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync<List<TEntity>>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public async Task<TEntity> PostAsync(string url)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody(new TEntity());
            var response = await _client.ExecuteAsync<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }


        public async Task<string> PostAsync(string url, TEntity entity)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody(JsonConvert.SerializeObject(entity));
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public async Task<string> PostsAsync(string url, List<TEntity> entities)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public async Task<TEntity> PostAsync(string baseUrl, string url, object entities)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = await client.ExecuteAsync<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public async Task<string> PutAsync(string url, TEntity entity)
        {
            var request = new RestRequest(url, Method.Put);
            request.AddJsonBody(JsonConvert.SerializeObject(entity));
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public async Task<TEntity> PutAsync(string url)
        {
            var request = new RestRequest(url, Method.Put);
            var response = await _client.ExecuteAsync<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public async Task<string> PutAsync(string url, List<TEntity> entities)
        {
            var request = new RestRequest(url, Method.Put);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public async Task<TEntity> PutAsync(string baseUrl, string url, object entities)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Put);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = await client.ExecuteAsync<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public async Task<string> DeleteAsync(string url)
        {
            var request = new RestRequest(url, Method.Delete);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public async Task<TEntity> DeletedAsync(string url)
        {
            var request = new RestRequest(url, Method.Delete);
            var response = await _client.ExecuteAsync<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public async Task<string> DeletesAsync(string url, List<TEntity> entities)
        {
            var request = new RestRequest(url, Method.Delete);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public async Task<TEntity> DeleteAsync(string baseUrl, string url, object entities)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Delete);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = await client.ExecuteAsync<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public async Task<string> PatchAsync(string url, TEntity entity)
        {
            var request = new RestRequest(url, Method.Patch);
            request.AddJsonBody(JsonConvert.SerializeObject(entity));
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public async Task<string> PatchAsync(string url, List<TEntity> entities)
        {
            var request = new RestRequest(url, Method.Patch);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

        public async Task<TEntity> PatchAsync(string baseUrl, string url, object entities)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.Patch);
            request.AddJsonBody(JsonConvert.SerializeObject(entities));
            var response = await client.ExecuteAsync<TEntity>(request);
            if (response.IsSuccessful)
                return response.Data;
            throw new IntegrationException(response.Content);
        }

        public async Task<string> HeadAsync(string url)
        {
            var request = new RestRequest(url, Method.Head);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Headers.ToString();
            throw new IntegrationException(response.Content);
        }

        public async Task<string> HeadsAsync(string url)
        {
            var request = new RestRequest(url, Method.Head);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new IntegrationException(response.Content);
        }

    }
}