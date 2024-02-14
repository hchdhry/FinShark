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
            MarkeyCap = stock.MarkeyCap,
            Purchase = stock.Purchase,
            Industry = stock.Industry
                        
            };
            
        }
    }
}