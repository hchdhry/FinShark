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

    public CommentController(ICommentRepository commentRepository)
    {
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


    }
}
