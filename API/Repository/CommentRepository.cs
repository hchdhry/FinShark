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

        public async Task<List<Comment>> GetAll()
        {
           return await _DBContext.Comments.ToListAsync();
        }

        public async Task<Comment> GetAsync(int id)
        {
            return await _DBContext.Comments.FindAsync(id);
        }
      
    }
}
