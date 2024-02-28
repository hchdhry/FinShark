using API.Data;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDBContext _dbcontext;
   
    public PortfolioRepository(ApplicationDBContext dBContext)
    {
        _dbcontext = dBContext;
      
    }
    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return await _dbcontext.portfolios.Where(u => u.AppUserId == user.Id)
        .Select(stock => new Stock{
            Id = stock.StockId,
            Symbol = stock.stock.Symbol,
            CompanyName = stock.stock.CompanyName, 
            Purchase = stock.stock.Purchase, 
            Industry = stock.stock.Industry,
            MarkeyCap = stock.stock.MarkeyCap  
            
        }).ToListAsync();
        
    }
}
