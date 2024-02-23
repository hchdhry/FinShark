using API.interfaces;
using API.Models;
using API.Data;
using Microsoft.EntityFrameworkCore;
namespace API.Repository{

    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _DBContext;
        public CommentRepository(ApplicationDBContext DBContext)
        {
            _DBContext =  DBContext;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _DBContext.Comments.AddAsync(comment);
            await _DBContext.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> DeleteAsync(int id)
        {
            Comment commentToDelete = await _DBContext.Comments.FirstOrDefaultAsync(u => u.Id == id);
            if(commentToDelete == null)
            {
                return null;
            }
            _DBContext.Comments.Remove(commentToDelete);
            _DBContext.SaveChangesAsync();
            return commentToDelete;
            
        }

        public async Task<List<Comment>> GetAll()
        {
           return await _DBContext.Comments.ToListAsync();
        }

        public async Task<Comment> GetAsync(int id)
        {
            return await _DBContext.Comments.FindAsync(id);
        }

        public async Task<Comment> UpdateAsync(int id, Comment comment)
        {
           var CommentToUpdate = await _DBContext.Comments.FindAsync(id);
        if(CommentToUpdate == null)
            {
                return null;
            }
            CommentToUpdate.Title = comment.Title;
            CommentToUpdate.Content = comment.Content;
            await _DBContext.SaveChangesAsync();

            return CommentToUpdate;
        }
    }
}
