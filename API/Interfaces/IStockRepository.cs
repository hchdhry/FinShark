using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> getAllAsync();
        
    }
}



