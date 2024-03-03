using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Mappers;
using API.Dtos.Comment;
using API.Dtos.Stock;
using API.Repository;
using API.interfaces;
using Microsoft.AspNetCore.Identity;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers{
    [Route("api/Comment")]
    [ApiController]
    public class CommentController:ControllerBase
{
    private readonly ICommentRepository _CommentRepo;
    private readonly IStockRepository _StockRepo;
    private readonly UserManager<AppUser> _userManager;

    private readonly IFMPService _fMPService;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository,UserManager<AppUser> userManager, IFMPService fMP)
    {
        _StockRepo   = stockRepository;
        _CommentRepo = commentRepository;
        _userManager = userManager;
        _fMPService = fMP;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var  Comment = await _CommentRepo.GetAsync(id);
        if(Comment == null)
        {
            return NotFound();
        }
        return Ok(Comment.ToCommentDto());
    }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var allComments = await _CommentRepo.GetAll();
            if (allComments == null)
            {
                return NotFound();
            }
            var CommentDtos = allComments.Select(Comment => Comment.ToCommentDto());
            return Ok(CommentDtos);


        }

        [HttpPost]
        [Route("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _StockRepo.getBySymbol(symbol);

            if (stock == null)
            {
                stock = await _fMPService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _StockRepo.CreateAsync(stock);
                }
            }

            var username = User.getUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = commentDto.ToCommentFromCreate(stock.Id);
            commentModel.AppUser = appUser;
            await _CommentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpDelete]
        [Route("id")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentToDelete = await _CommentRepo.DeleteAsync(id);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateCommentDto dto )
        {
            var comment = await _CommentRepo.UpdateAsync(id,dto.ToCommentFromUpdate());
            if(comment == null)
            {
               return NotFound("not found");

            }
            return Ok(comment.ToCommentDto());
        }

       
        



    }
}
