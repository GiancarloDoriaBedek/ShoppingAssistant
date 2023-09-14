using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAssistant.DTOs;
using ShoppingAssistant.DTOs.ScraperDTOs;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;
using ShoppingAssistant.Services.Interfaces;
using System.Data;
using System.Net;

namespace ShoppingAssistant.Controllers
{
    [Route("api/Scraper")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ScraperController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IWebshopScraperService _webshopScraperService;
        private readonly IProductMappingService _productMappingService;

        private ApiResponseDTO _response;

        public ScraperController(
            IProductRepository productRepository,
            IMapper mapper,
            IWebshopScraperService webshopScraperService,
            IProductMappingService productMappingService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _webshopScraperService = webshopScraperService;

            _response = new ApiResponseDTO();
            _productMappingService = productMappingService;
        }

        [HttpPost("ScrapeWebstore")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponseDTO>> ScrapeWebshop(ScrapeWebshopRequestDTO request)
        {
            try
            {
                var scrapeCategoriesRequest = _mapper.Map<ScrapeCategoriesRequestDTO>(request);
                var scrapedCategories = await _webshopScraperService.ScrapeCategories(scrapeCategoriesRequest);

                var scrapePagesRequest = _mapper.Map<List<ScrapePagesRequestDTO>>(scrapedCategories);

                scrapePagesRequest
                    .ForEach(x => x.StoreName = request.StoreName);

                var scrapedPages = new List<ScrapePagesResponseDTO>();

                Parallel.ForEach(
                    scrapePagesRequest,
                    x =>
                    {
                        var pagesOnCategory = _webshopScraperService.ScrapePages(x);
                        if (pagesOnCategory.Result is not null)
                            scrapedPages.AddRange(pagesOnCategory.Result);
                    });

                var scrapeProductsRequest = _mapper.Map<List<ScrapeProductsRequestDTO>>(scrapedPages);
                foreach (var requestDTO in scrapeProductsRequest)
                {
                    requestDTO.StoreName = request.StoreName;
                }

                var scrapedProducts = new List<ScrapeProductsResponseDTO>();

                Parallel.ForEach(
                    scrapeProductsRequest,
                    x =>
                    {
                        var productsOnPage = _webshopScraperService.ScrapeProducts(x);
                        if (productsOnPage.Result is not null)
                            scrapedProducts.AddRange(productsOnPage.Result);
                    });

                var products = _productMappingService.MapToProduct(scrapedProducts);

                foreach (var product in products)
                {
                    await _productRepository.SaveProduct(product);
                }

                _response.Result = scrapedProducts;
                _response.StatusCode = HttpStatusCode.Created;
                return Created("Did it", _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
