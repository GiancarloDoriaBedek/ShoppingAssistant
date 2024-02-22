using ShoppingAssistant.DTOs.ProductDTOs;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.Repository.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProducts(GetProductsRequestDTO request);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task SaveProduct(Product product);
    }
}
