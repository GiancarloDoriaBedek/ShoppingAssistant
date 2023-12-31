﻿using ShoppingAssistant.Models;

namespace ShoppingAssistant.Repository.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //Task<IEnumerable<Product>> GetProductsOfSimilarNameAsync(string similarName);
        //Task<Product> GetProduct(string similarName, string storeName);

        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task SaveProduct(Product product);
    }
}
