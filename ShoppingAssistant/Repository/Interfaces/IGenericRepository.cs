﻿using System.Linq.Expressions;

namespace ShoppingAssistant.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null, 
            string includeProperties = null);

        Task<T> GetAsync(
            Expression<Func<T, bool>> filter = null, 
            bool tracked = true, 
            string includeProperties = null);

        Task CreateAsync(T entity);

        Task RemoveAsync(T entity);

        Task SaveAsync();
    }
}
