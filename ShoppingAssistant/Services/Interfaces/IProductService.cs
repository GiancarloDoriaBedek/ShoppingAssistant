using ShoppingAssistant.DTOs.ProductDTOs;

namespace ShoppingAssistant.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<GetProductsResponseDTO>> GetProducts(GetProductsRequestDTO filter);
    }
}
