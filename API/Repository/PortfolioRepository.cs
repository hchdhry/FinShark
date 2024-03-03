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

    public async Task<Portfolio> CreateAsync(Portfolio portfolio)
    {
      await _dbcontext.portfolios.AddAsync(portfolio);
      await _dbcontext.SaveChangesAsync();
      return portfolio;
    }

    public async Task<Portfolio> DeleteAsync(AppUser appUser, string symbol)
    {
        var portfolioModel = await _dbcontext.portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.stock.Symbol.ToLower() == symbol.ToLower());

        if (portfolioModel == null)
        {
            return null;
        }

        _dbcontext.portfolios.Remove(portfolioModel);
        await _dbcontext.SaveChangesAsync();
        return portfolioModel;
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
            MarketCap = stock.stock.MarketCap  
            
        }).ToListAsync();
        
    }

   
}
