using API.Dtos.Stock;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.interfaces
{
    public interface IStockRepository
    {
         Task<List<Stock>> getAllAsync(QueryObject query);

        Task<Stock?> getById(int id);
        Task<Stock?> getBySymbol(string symbol);

        Task<Stock> CreateAsync(Stock stock);

        Task<Stock> UpdateAsync(int id, UpdateStockDto stock);

        public Task<Stock> DeleteAsync(int id);

        public Task<bool> StockExistsAsync(int id);
        
    }
}



