using API.Dtos.Stock;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> getAllAsync();

        public Task<Stock?> getById(int id);

        public Task<Stock> CreateAsync(Stock stock);

        public Task<Stock> UpdateAsync(int id, UpdateStockDto stock);

        public Task<Stock> DeleteAsync(int id);
        
    }
}



