using API.Data;
using API.interfaces;
using API.Models;
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
        public Task<List<Stock>> getAllAsync()
        {
            return  _DBContext.Stock.ToListAsync();
        }
    }
}