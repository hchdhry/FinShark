using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Mappers;
using API.Dtos.Stock;
using API.Repository;
using API.interfaces;
using API.Helpers;
namespace API.Controllers{
    [Route("api/stock")]
    [ApiController]
    public class StockController:ControllerBase
{
    private readonly ApplicationDBContext _dbcontext;
        private readonly IStockRepository _Stockrepo;
        public StockController(ApplicationDBContext Context, IStockRepository stockRepository)
   {
    _dbcontext = Context;
    _Stockrepo =stockRepository;
   }
   [HttpGet]
   public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
   {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };
            var allstocks = await _Stockrepo.getAllAsync(query);
    
    var StockDto = allstocks.Select(stock => stock.ToStockDto());
    return Ok(StockDto);
   }
   [HttpGet("{id:int}")]

   public async Task<IActionResult> GetById([FromRoute]int id)
   {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _Stockrepo.getById(id);
       
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
   }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = stockDto.ToStockFromCreateDto();
      await _Stockrepo.CreateAsync(stock);
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
      
    }

    [HttpPut]
    [Route("{id:int}")]

    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto stock)
    {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockToUpdate =await _Stockrepo.UpdateAsync(id,stock);
        if(stockToUpdate == null)
        {
            return NotFound();
        }
        return Ok(stockToUpdate.ToStockDto());
    }
[HttpDelete]
[Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Stock StockToDelete = await _Stockrepo.DeleteAsync(id);
        if (StockToDelete == null)
        {
            return NotFound();
        }
        return NoContent();
    }




}
}
