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
                MarkeyCap = stockDto.MarkeyCap,
                Purchase = stockDto.Purchase,
                Industry = stockDto.Industry
            };

        }
    }
}