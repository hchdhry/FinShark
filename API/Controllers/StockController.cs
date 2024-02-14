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



}
}
