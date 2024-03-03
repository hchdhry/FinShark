using API.Models;

namespace API;

public interface IFMPService
{
    Task<Stock> FindStockBySymbolAsync(string symbol);

}
