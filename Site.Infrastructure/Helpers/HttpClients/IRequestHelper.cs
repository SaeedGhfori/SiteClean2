using System.Collections.Generic;

namespace Site.Infrastructure.Helpers.HttpClients
{
    public interface IRequestHelper<TEntity>
    {
        TEntity Get(string url);
        string Geturl(string url);
        List<TEntity> GetCollection(string url);
        string Post(string url, TEntity entity);
        string Posts(string url, List<TEntity> entity);
        TEntity Post(string baseUrl, string url, object entities);
        TEntity Post(string url);
        string Put(string url, TEntity entity);
        TEntity Put(string url);
        string Put(string url, List<TEntity> entities);
        TEntity Put(string baseUrl, string url, object entities);
        string Delete(string url);
        TEntity Deleted(string url);
        string Deletes(string url, List<TEntity> entities);
        TEntity Delete(string baseUrl, string url, object entities);
        string Patch(string url, TEntity entity);
        string Patch(string url, List<TEntity> entities);
        TEntity Patch(string baseUrl, string url, object entities);
        string Head(string url);
        string Heads(string url);


        //Method asynchronous 
        Task<TEntity> GetAsync(string url);
        Task<string> GeturlAsync(string url);
        Task<List<TEntity>> GetCollectionAsync(string url);
        Task<TEntity> PostAsync(string url);
        Task<string> PostAsync(string url, TEntity entity);
        Task<string> PostsAsync(string url, List<TEntity> entities);
        Task<TEntity> PostAsync(string baseUrl, string url, object entities);
        Task<string> PutAsync(string url, TEntity entity);
        Task<TEntity> PutAsync(string url);
        Task<string> PutAsync(string url, List<TEntity> entities);
        Task<TEntity> PutAsync(string baseUrl, string url, object entities);
        Task<string> DeleteAsync(string url);
        Task<TEntity> DeletedAsync(string url);
        Task<string> DeletesAsync(string url, List<TEntity> entities);
        Task<TEntity> DeleteAsync(string baseUrl, string url, object entities);
        Task<string> PatchAsync(string url, TEntity entity);
        Task<string> PatchAsync(string url, List<TEntity> entities);
        Task<TEntity> PatchAsync(string baseUrl, string url, object entities);
        Task<string> HeadAsync(string url);
        Task<string> HeadsAsync(string url);

    }
}