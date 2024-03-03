using System.Linq.Expressions;
using API.Models;
using API.Dtos.Stock;
using API.Mappers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;

namespace API;

public class FMPService : IFMPService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
        }
    public async Task<Stock> FindStockBySymbolAsync(string symbol)
    {
        try
        {
            var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config["FMPKey"]}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<FMPStock[]>(content);
                var stock = task[0];
                if (stock != null)
                {
                    return stock.ToStockFromFMP();
                }
                return null;
            }
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
        }
    }

