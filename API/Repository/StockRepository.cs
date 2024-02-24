using API.Data;
using API.Dtos.Stock;
using API.Helpers;
using API.interfaces;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace API.Repository
{

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
            if (StockToDelete == null)
            {
                return null;
            }
            _DBContext.Stock.Remove(StockToDelete);
            await _DBContext.SaveChangesAsync();
            return StockToDelete;

        }

        public async Task<List<Stock>> getAllAsync(QueryObject query)
        {
            var stock = _DBContext.Stock.Include(c => c.comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stock = stock.Where(c => c.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stock = stock.Where(c => c.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stock = query.IsDescending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                }
            }
            return await stock.ToListAsync();
        }

        public async Task<Stock?> getById(int id)
        {
            return await _DBContext.Stock.Include(c => c.comments).FirstOrDefaultAsync(c => c.Id == id);

        }

        public Task<bool> StockExistsAsync(int id)
        {
            return _DBContext.Stock.AnyAsync(u => u.Id == id);

        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockDto stock)
        {
            var stockToUpdate = await _DBContext.Stock.FirstOrDefaultAsync(u => u.Id == id);
            if (stockToUpdate == null)
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