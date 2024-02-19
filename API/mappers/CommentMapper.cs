using API.Dtos.Comment;
using API.Models;


namespace API.Mappers{

public static class CommentMapper
{

    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            StockId = comment.StockId
        };
    }

}
}