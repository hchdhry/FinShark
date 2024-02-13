using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;
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
    var allstocks =_dbcontext.Stock.ToList();
    return Ok(allstocks);
   }
   [HttpGet("{id}")]

   public IActionResult GetById([FromRoute]int id)
   {
        Stock stock = _dbcontext.Stock.Find(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock);
   }



}
}
