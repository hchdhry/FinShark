using API.Models;

namespace API.interfaces{

public interface ICommentRepository
{

    public Task<List<Comment>> GetAll();

    public Task<Comment> GetAsync(int id);

    public Task<Comment> CreateAsync(Comment comment);

    public Task<Comment> DeleteAsync(int id);
    

}
}
