using Microsoft.Extensions.Configuration;
using Site.Application.Definitions.Contracts.Interfaces.Products;
using Site.Application.Definitions.Models.Products;
using Site.Infrastructure.Helpers.RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IRestClient<Product> _restClient;
        private readonly string _productBaseUrl;

        public ProductService(IRestClient<Product> restClient, IConfiguration configuration)
        {
            _restClient = restClient;
            _productBaseUrl = configuration.GetValue<string>("RestClientSettings:ProductBaseUrl");
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {

            var url = $"Product";

            try
            {
                var result = await _restClient.GetCollectionAsync(_productBaseUrl + url);
                return result ?? new List<Product>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }

            var url = $"Product/{id}";

            try
            {
                var result = await _restClient.GetAsync(_productBaseUrl + url);
                return result ?? new Product();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Product> CreateAsync(Product product)
        {
            if (product.Price == null)
            {
                throw new ArgumentNullException();
            }

            var url = $"Product";

            try
            {
                var result = await _restClient.PostAsync(_productBaseUrl, url, product);
                return result ?? new Product();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            if (product.Price == null)
            {
                throw new ArgumentNullException();
            }

            var url = $"Product/{id}";

            try
            {
                var result = await _restClient.PutAsync(_productBaseUrl, url, product);
                return result ?? new Product();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }

            var url = $"Product/{id}";

            try
            {
                await _restClient.DeleteAsync(_productBaseUrl + url);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }

}
