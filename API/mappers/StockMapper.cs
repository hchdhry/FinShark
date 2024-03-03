using API.Models;
using API.Dtos;
using API.Dtos.Stock;
namespace API.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto()
            {
             Id = stock.Id,
            LastDiv = stock.LastDiv,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            MarketCap = stock.MarketCap,
            Purchase = stock.Purchase,
            Industry = stock.Industry,
            Comments = stock.comments.Select(c=>c.ToCommentDto()).ToList()
                        
            };
            
        }
        public static Stock ToStockFromCreateDto(this CreateStockDto stockDto)
        {
           return new Stock
            {
                LastDiv = stockDto.LastDiv,
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                MarketCap = stockDto.MarketCap,
                Purchase = stockDto.Purchase,
                Industry = stockDto.Industry
            };

        }
        public static Stock ToStockFromFMP(this FMPStock fmpStock)
        {
            return new Stock
            {
                Symbol = fmpStock.symbol,
                CompanyName = fmpStock.companyName,
                Purchase = (decimal)fmpStock.price,
                LastDiv = (decimal)fmpStock.lastDiv,
                Industry = fmpStock.industry,
                MarketCap = fmpStock.mktCap
            };
        }
    }
}