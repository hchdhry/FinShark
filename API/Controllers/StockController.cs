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
   public async Task<IActionResult> GetAll()
   {
    var allstocks = await _dbcontext.Stock.ToListAsync();
    
    var StockDto = allstocks.Select(stock => stock.ToStockDto());
    return Ok(StockDto);
   }
   [HttpGet("{id}")]

   public async Task<IActionResult> GetById([FromRoute]int id)
   {
        var stock = await _dbcontext.Stock.FindAsync(id);
       
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
   }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
    {
        var stock = stockDto.ToStockFromCreateDto();
       await _dbcontext.Stock.AddAsync(stock);
        await _dbcontext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
      
    }

    [HttpPut]
    [Route("{id}")]

    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto stock)
    {
        var stockToUpdate =await _dbcontext.Stock.FirstOrDefaultAsync(u=>u.Id == id);
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
       await _dbcontext.SaveChangesAsync();
        return Ok(stockToUpdate.ToStockDto());
    }
[HttpDelete]
[Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        Stock StockToDelete = await _dbcontext.Stock.FirstOrDefaultAsync(u => u.Id == id);
        if (StockToDelete == null)
        {
            return NotFound();
        }
        _dbcontext.Stock.Remove(StockToDelete);
        await _dbcontext.SaveChangesAsync();
        return NoContent();
    }




}
}
