using Newtonsoft.Json;
using ShoppingAssistant.DTOs.ScraperDTOs;
using ShoppingAssistant.Models;
using ShoppingAssistant.Services.Interfaces;
using System.Text;

namespace ShoppingAssistant.Services.Implementations
{
    public class WebshopScraperService : IWebshopScraperService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public WebshopScraperService(
            HttpClient httpClient, 
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ScrapeCategoriesResponseDTO>> ScrapeCategories(ScrapeCategoriesRequestDTO request)
        {
            var endpointUrl = GetScraperAPIEndpointFromConfiguration("ProductCategories");

            try
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(endpointUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<List<ScrapeCategoriesResponseDTO>>(responseData);

                    if (categories is null)
                    {
                        throw new Exception("No products found");
                    }

                    return categories;
                }
                else
                {
                    throw new Exception("External API is not responding");
                }
            }
            catch (Exception ex)
            {
                // Log exception message

                return new List<ScrapeCategoriesResponseDTO>();
            }
        }

        public async Task<IEnumerable<ScrapePagesResponseDTO>> ScrapePages(ScrapePagesRequestDTO request)
        {
            var endpointUrl = GetScraperAPIEndpointFromConfiguration("ScrapePages");

            try
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(endpointUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var pages = JsonConvert.DeserializeObject<List<ScrapePagesResponseDTO>>(responseData);

                    if (pages is null)
                    {
                        throw new Exception("No products found");
                    }

                    return pages;
                }
                else
                {
                    throw new Exception("External API is not responding");
                }
            }
            catch (Exception ex)
            {
                // Log exception message

                return new List<ScrapePagesResponseDTO>();
            }
        }

        public async Task<IEnumerable<ScrapeProductsResponseDTO>> ScrapeProducts(ScrapeProductsRequestDTO request)
        {
            var endpointUrl = GetScraperAPIEndpointFromConfiguration("ScrapeProducts");

            try
            {
                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(endpointUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<List<ScrapeProductsResponseDTO>>(responseData);

                    if (products is null)
                    {
                        return new List<ScrapeProductsResponseDTO>();
                    }

                    return products;
                }
                else
                {
                    throw new Exception("External API is not responding");
                }
            }
            catch (Exception ex)
            {
                // Log exception message

                return new List<ScrapeProductsResponseDTO>();
            }
        }

        private string GetScraperAPIEndpointFromConfiguration(string endpointName)
        {
            var apiUrl = _configuration.GetSection("AppSettings:ScraperAPI:URL").Value;
            var endpoint = string.Empty;

            switch (endpointName)
            {
                case "ProductCategories":
                    endpoint = _configuration.GetSection("AppSettings:ScraperAPI:ScrapeCategories").Value;
                    return apiUrl + "ProductCategories/";

                case "ScrapePages":
                    endpoint = _configuration.GetSection("AppSettings:ScraperAPI:ScrapePages").Value;
                    return apiUrl + "Pages/";

                case "ScrapeProducts":
                    endpoint = _configuration.GetSection("AppSettings:ScraperAPI:ScrapeProducts").Value;
                    return apiUrl + "Products/";

                default:
                    throw new NotImplementedException(
                        $"Endpoint with name {endpointName} either " +
                        $"does not exist or is not implemented at the moment");
            }

        }
    }
}
