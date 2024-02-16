using API.Data;
using API.Dtos.Stock;
using API.interfaces;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace API.Repository{

    public class StockRepository : IStockRepository
    {
        private readonly IStockRepository _StockRepo;
        private readonly ApplicationDBContext _DBContext;
        public StockRepository(ApplicationDBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _DBContext.Stock.AddAsync(stock);
            await _DBContext.SaveChangesAsync();
            return stock;

        }

        public async Task<Stock> DeleteAsync(int id)
        {
            var StockToDelete = await _DBContext.Stock.FirstOrDefaultAsync(u => u.Id == id);
            if(StockToDelete == null)
            {
                return null;
            }
             _DBContext.Stock.Remove(StockToDelete);
             await _DBContext.SaveChangesAsync();
             return StockToDelete;

        }

        public Task<List<Stock>> getAllAsync()
        {
            return  _DBContext.Stock.ToListAsync();
        }

        public async Task<Stock?> getById(int id)
        {
           return await _DBContext.Stock.FindAsync(id);
         
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockDto stock)
        {
           var stockToUpdate = await _DBContext.Stock.FirstOrDefaultAsync(u => u.Id==id);
           if(stockToUpdate == null)
           {
            return null;
           }
            stockToUpdate.LastDiv = stock.LastDiv;
            stockToUpdate.Symbol = stock.Symbol;
            stockToUpdate.CompanyName = stock.CompanyName;
            stockToUpdate.MarkeyCap = stock.MarkeyCap;
            stockToUpdate.Purchase = stock.Purchase;
            stockToUpdate.Industry = stock.Industry;
            await _DBContext.SaveChangesAsync();
            return stockToUpdate;
        }
    }
}