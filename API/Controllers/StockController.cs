using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Mappers;
using API.Dtos.Stock;
namespace API.Controllers{
    [Route("api/stock")]
    [ApiController]
    public class StockController:ControllerBase
{
    private readonly ApplicationDBContext _dbcontext;
   public StockController(ApplicationDBContext Context)
   {
    _dbcontext = Context;
   }
   [HttpGet]
   public IActionResult GetAll()
   {
    var allstocks =_dbcontext.Stock.ToList().Select(stock => stock.ToStockDto());
    return Ok(allstocks);
   }
   [HttpGet("{id}")]

   public IActionResult GetById([FromRoute]int id)
   {
        StockDto? stock = _dbcontext.Stock.Find(id).ToStockDto();
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock);
   }
    [HttpPost]
    public IActionResult Create([FromBody] CreateStockDto stockDto)
    {
        var stock = stockDto.ToStockFromCreateDto();
        _dbcontext.Stock.Add(stock);
        _dbcontext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
      
    }

    [HttpPut]
    [Route("{id}")]

    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockDto stock)
    {
        var stockToUpdate = _dbcontext.Stock.FirstOrDefault(u=>u.Id == id);
        if(stockToUpdate == null)
        {
            return NotFound();
        }
        stockToUpdate.LastDiv = stock.LastDiv;
        stockToUpdate.Symbol = stock.Symbol;
        stockToUpdate.CompanyName = stock.CompanyName;
        stockToUpdate.MarkeyCap = stock.MarkeyCap;
        stockToUpdate.Purchase = stock.Purchase;
        stockToUpdate.Industry = stock.Industry;
        _dbcontext.SaveChanges();
        return Ok(stockToUpdate.ToStockDto());
    }
[HttpDelete]
[Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        Stock StockToDelete = _dbcontext.Stock.FirstOrDefault(u => u.Id == id);
        if (StockToDelete == null)
        {
            return NotFound();
        }
        _dbcontext.Stock.Remove(StockToDelete);
        _dbcontext.SaveChanges();
        return NoContent();
    }




}
}
