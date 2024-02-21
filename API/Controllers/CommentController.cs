using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Mappers;
using API.Dtos.Stock;
using API.Repository;
using API.interfaces;

namespace API.Controllers{
    [Route("api/Comment")]
    [ApiController]
    public class CommentController:ControllerBase
{
    private readonly ICommentRepository _CommentRepo;
    private readonly IStockRepository _StockRepo;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _StockRepo   = stockRepository;
        _CommentRepo = commentRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
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
            var allComments = await _CommentRepo.GetAll();
            if (allComments == null)
            {
                return NotFound();
            }
            var CommentDtos = allComments.Select(Comment => Comment.ToCommentDto());
            return Ok(CommentDtos);


        }
        [HttpPost("{StockId}")]
        public async Task<IActionResult> Create([FromRoute]int StockId,[FromBody]CreateCommentDto comment)
        {
            if(!await _StockRepo.StockExistsAsync(StockId))
            {
                return BadRequest("stock does not exist");
            }
            var CreatedComment = comment.ToCommentFromCreate(StockId);
            await _CommentRepo.CreateAsync(CreatedComment);
            return CreatedAtAction(nameof(GetById), new { id = CreatedComment.Id }, CreatedComment.ToCommentDto());
        }
        [HttpDelete]
        [Route("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var commentToDelete = await _CommentRepo.DeleteAsync(id);
            if (commentToDelete == null)
            {
                return NotFound();
            }
            return Ok(commentToDelete);
        }



    }
}
