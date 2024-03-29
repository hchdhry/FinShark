﻿using API.Dtos.Comment;
using API.Helpers;
using API.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace API.interfaces{

public interface ICommentRepository
{

    public Task<List<Comment>> GetAll(CommentQueryObject queryObject);

    public Task<Comment> GetAsync(int id);

    public Task<Comment> CreateAsync(Comment comment);

    public Task<Comment> DeleteAsync(int id);

    public Task<Comment> UpdateAsync(int id,Comment comment);
    

}
}
